using InstrumentService.Business.Models;
using InstrumentService.Business.Models.Request;
using InstrumentService.Business.Models.Response;

namespace InstrumentService.Business.Abstractions;

public interface IInstrumentService
{
    Task<InstrumentResponseModel> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task<PaginatedModel<InstrumentResponseModel>> GetPagedAsync(int page, int pageSize,
        CancellationToken cancellationToken);

    Task<List<InstrumentResponseModel>> GetTopAsync(int limit, CancellationToken cancellationToken);

    Task<InstrumentResponseModel> CreateAsync(string? userId, InstrumentRequestModel requestModel,
        CancellationToken cancellationToken);

    Task UpdateAsync(string? userId, string instrumentId, InstrumentRequestModel request,
        CancellationToken cancellationToken);

    Task<string> DeleteAsync(string? userId, string instrumentId, CancellationToken cancellationToken);
    Task<List<InstrumentTypeResponseModel>> GetTypesAsync(CancellationToken cancellationToken);
    Task<List<FormFieldDescriptorResponseModel>> GetFieldsByTypeAsync(string type, CancellationToken cancellationToken);

    Task<PaginatedModel<UserInstrumentResponseModel>> GetPagedUserInstrumentsAsync(int page, int pageSize,
        string? userId,
        CancellationToken cancellationToken);

    Task<UserContactsModel> GetOwnerContactsAsync(string instrumentId, CancellationToken cancellationToken);
}