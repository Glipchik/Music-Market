using AutoMapper;
using InstrumentService.Business.Abstractions;
using InstrumentService.Business.Common;
using InstrumentService.Business.Models;
using InstrumentService.Business.Models.Request;
using InstrumentService.Business.Models.Response;
using InstrumentService.DataAccess.Abstractions;
using InstrumentService.DataAccess.Entities;
using MassTransit;
using Shared.Exceptions;
using Shared.Messaging.Contracts.Events.Instrument;

namespace InstrumentService.Business.Services;

public class InstrumentService(
    IInstrumentRepository instrumentRepository,
    IInstrumentTypeRepository instrumentTypeRepository,
    IInstrumentFormMetadataService instrumentFormMetadataService,
    IInstrumentResponseModelFactory instrumentResponseModelFactory,
    IMapper mapper,
    ICloudStorage cloudStorage,
    IPublishEndpoint publishEndpoint,
    IAnalyticsClient analyticsClient,
    IUserClient userClient)
    : IInstrumentService
{
    public async Task<InstrumentResponseModel> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var instrument = await instrumentRepository.GetByIdAsync(id, cancellationToken);

        if (instrument is null)
        {
            throw new NotFoundException(ErrorMessages.InstrumentNotFound(id));
        }

        var instrumentModel = await instrumentResponseModelFactory.CreateAsync(instrument, cancellationToken);

        var today = DateOnly.FromDateTime(DateTime.Now);
        await publishEndpoint.Publish(new InstrumentViewed(id, today), cancellationToken);

        return instrumentModel;
    }

    public async Task<PaginatedModel<InstrumentResponseModel>> GetPagedAsync(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var skip = (page - 1) * pageSize;
        var totalCount = await instrumentRepository.CountAsync(cancellationToken);

        var instruments = await instrumentRepository.GetPagedAsync(skip, pageSize, cancellationToken);

        var instrumentTasks = instruments.Select(instrument =>
            instrumentResponseModelFactory.CreateAsync(instrument, cancellationToken));

        var instrumentModels = await Task.WhenAll(instrumentTasks);

        var paginatedInstrumentsModel = new PaginatedModel<InstrumentResponseModel>(
            instrumentModels.ToList(),
            totalCount,
            page,
            pageSize);

        return paginatedInstrumentsModel;
    }

    public async Task<List<InstrumentResponseModel>> GetTopAsync(int limit, CancellationToken cancellationToken)
    {
        var topInstruments = await analyticsClient.GetTopViewedInstrumentsAsync(limit, cancellationToken);

        if (!topInstruments.Any())
            return [];

        var instrumentIds = topInstruments.Select(topInstrument => topInstrument.InstrumentId).ToList();

        var instruments = await instrumentRepository.GetByIdRangeAsync(instrumentIds, cancellationToken);

        var instrumentTasks = instruments.Select(instrument =>
            instrumentResponseModelFactory.CreateAsync(instrument, cancellationToken));

        var instrumentModels = await Task.WhenAll(instrumentTasks);

        var instrumentModelsList = instrumentModels.ToList();

        var instrumentModelDict = instrumentModelsList.ToDictionary(instrumentModel => instrumentModel.Id);

        var instrumentResponseModels = topInstruments
            .Where(topInstrument => instrumentModelDict.ContainsKey(topInstrument.InstrumentId))
            .OrderByDescending(topInstrument => topInstrument.Views)
            .Select(topInstrument => instrumentModelDict[topInstrument.InstrumentId])
            .ToList();

        return instrumentResponseModels;
    }

    public async Task<InstrumentResponseModel> CreateAsync(string? userId, InstrumentRequestModel requestModel,
        CancellationToken cancellationToken)
    {
        if (userId is null)
        {
            throw new UnauthorizedException(ErrorMessages.UserIdIsMissing);
        }

        var instrument = mapper.Map<Instrument>(requestModel);
        instrument.OwnerId = userId;

        try
        {
            await instrumentRepository.AddAsync(instrument, cancellationToken);
            var today = DateOnly.FromDateTime(DateTime.Now);
            await publishEndpoint.Publish(new InstrumentCreated(instrument.Id, today), cancellationToken);

            var instrumentModel = await instrumentResponseModelFactory.CreateAsync(instrument, cancellationToken);

            return instrumentModel;
        }
        catch (Exception exception)
        {
            throw new Exception(ErrorMessages.FailedToSaveInstrument, exception);
        }
    }

    public async Task UpdateAsync(string? userId, string instrumentId, InstrumentRequestModel request,
        CancellationToken cancellationToken)
    {
        if (userId is null)
        {
            throw new UnauthorizedException(message: ErrorMessages.UserIdIsMissing);
        }

        var existingInstrument = await instrumentRepository.GetByIdAsync(instrumentId, cancellationToken);

        if (existingInstrument is null)
        {
            throw new NotFoundException(ErrorMessages.InstrumentNotFound(instrumentId));
        }

        if (existingInstrument.OwnerId != userId)
        {
            throw new ForbiddenException(ErrorMessages.ForbiddenAction);
        }

        var instrument = mapper.Map<Instrument>(request);
        instrument.Id = existingInstrument.Id;
        instrument.OwnerId = existingInstrument.OwnerId;

        await instrumentRepository.UpdateAsync(instrument.Id, instrument, cancellationToken);
    }

    public async Task<List<InstrumentTypeResponseModel>> GetTypesAsync(CancellationToken cancellationToken)
    {
        var types = await instrumentTypeRepository.GetAllAsync(cancellationToken);
        var typeModels = mapper.Map<List<InstrumentTypeResponseModel>>(types);

        return typeModels;
    }

    public async Task<List<FormFieldDescriptorResponseModel>> GetFieldsByTypeAsync(string type,
        CancellationToken cancellationToken)
    {
        var formFieldDescriptors = await instrumentFormMetadataService
            .GetCombinedFormFieldDescriptorsAsync(type, cancellationToken);
        var formFieldDescriptorModels = mapper.Map<List<FormFieldDescriptorResponseModel>>(formFieldDescriptors);

        return formFieldDescriptorModels;
    }

    public async Task<PaginatedModel<UserInstrumentResponseModel>> GetPagedUserInstrumentsAsync(int page, int pageSize,
        string? userId,
        CancellationToken cancellationToken)
    {
        if (userId is null)
        {
            throw new UnauthorizedException(ErrorMessages.UserIdIsMissing);
        }

        var skip = (page - 1) * pageSize;
        var totalCount = await instrumentRepository.CountByUserIdAsync(userId, cancellationToken);

        var instruments = await instrumentRepository.GetPagedByUserId(skip, pageSize, userId, cancellationToken);

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var sevenDaysAgo = today.AddDays(-6);

        var semaphore = new SemaphoreSlim(5);

        var tasks = instruments.Select(async instrument =>
        {
            await semaphore.WaitAsync(cancellationToken);

            try
            {
                var instrumentResponseModel =
                    await instrumentResponseModelFactory.CreateAsync(instrument, cancellationToken);

                try
                {
                    var totalStat = await analyticsClient.GetInstrumentStatAsync(instrument.Id, cancellationToken);
                    var dailyStats = await analyticsClient
                        .GetInstrumentDailyStatsForDateRangeAsync(instrument.Id, sevenDaysAgo, today,
                            cancellationToken);

                    var totalStatResponseModel = mapper.Map<InstrumentStatResponseModel>(totalStat);
                    var dailyStatsResponseModel = mapper.Map<List<InstrumentDailyStatResponseModel>>(dailyStats);

                    var userInstrumentResponseModel = mapper.Map<UserInstrumentResponseModel>(instrumentResponseModel);
                    userInstrumentResponseModel = userInstrumentResponseModel with
                    {
                        TotalStats = totalStatResponseModel,
                        DailyStats = dailyStatsResponseModel
                    };

                    return userInstrumentResponseModel;
                }
                catch (Exception exception)
                {
                    throw new HttpRequestException(ErrorMessages.FailedToFetchInstrumentStats(instrument.Id),
                        exception);
                }
            }
            finally
            {
                semaphore.Release();
            }
        });

        var userInstrumentModels = await Task.WhenAll(tasks);

        var paginatedInstrumentsModel = new PaginatedModel<UserInstrumentResponseModel>(
            userInstrumentModels.ToList(),
            totalCount,
            page,
            pageSize);

        return paginatedInstrumentsModel;
    }

    public async Task<UserContactsModel> GetOwnerContactsAsync(string instrumentId, CancellationToken cancellationToken)
    {
        var instrument = await instrumentRepository.GetByIdAsync(instrumentId, cancellationToken);

        if (instrument is null)
        {
            throw new NotFoundException(ErrorMessages.InstrumentNotFound(instrumentId));
        }

        var contact = await userClient.GetUserContactsAsync(instrument.OwnerId, cancellationToken);
        var contactModel = mapper.Map<UserContactsModel>(contact);

        await publishEndpoint.Publish(new InstrumentContactViewed(instrumentId), cancellationToken);

        return contactModel;
    }

    public async Task<string> DeleteAsync(string? userId, string instrumentId, CancellationToken cancellationToken)
    {
        if (userId is null)
        {
            throw new UnauthorizedException(ErrorMessages.UserIdIsMissing);
        }

        var instrument = await instrumentRepository.GetByIdAsync(instrumentId, cancellationToken);

        if (instrument is null)
        {
            throw new NotFoundException(ErrorMessages.InstrumentNotFound(instrumentId));
        }

        if (instrument.OwnerId != userId)
        {
            throw new ForbiddenException(ErrorMessages.ForbiddenAction);
        }

        if (instrument.PhotoNames.Count != 0)
        {
            await cloudStorage.DeleteFilesAsync(instrument.PhotoNames, cancellationToken);
        }

        await instrumentRepository.DeleteAsync(instrumentId, cancellationToken);

        await publishEndpoint.Publish(new InstrumentDeleted(instrumentId), cancellationToken);

        return instrumentId;
    }
}