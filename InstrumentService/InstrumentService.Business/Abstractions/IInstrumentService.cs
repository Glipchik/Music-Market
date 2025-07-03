using InstrumentService.Business.Models;
using InstrumentService.Business.Models.Request;
using InstrumentService.Business.Models.Response;

namespace InstrumentService.Business.Abstractions;

public interface IInstrumentService
{
    Task<InstrumentResponseModel> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<List<InstrumentResponseModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<InstrumentResponseModel>> GetTopAsync(int limit, CancellationToken cancellationToken);
    Task<InstrumentResponseModel> CreateAsync(string? userId, InstrumentRequestModel requestModel, CancellationToken cancellationToken);

    Task UpdateAsync(string? userId, string instrumentId, InstrumentRequestModel request,
        CancellationToken cancellationToken);

    Task<string> DeleteAsync(string? userId, string instrumentId, CancellationToken cancellationToken);
    Task<List<InstrumentTypeResponseModel>> GetTypesAsync(CancellationToken cancellationToken);
    Task<List<FormFieldDescriptorResponseModel>> GetFieldsByTypeAsync(string type, CancellationToken cancellationToken);

    Task<List<UserInstrumentResponseModel>> GetAllUserInstrumentsAsync(string? userId,
        CancellationToken cancellationToken);

    Task<UserContactsModel> GetOwnerContactsAsync(string instrumentId, CancellationToken cancellationToken);
}