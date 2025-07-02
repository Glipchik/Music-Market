using InstrumentService.Business.Models.Response;
using InstrumentService.DataAccess.Entities;

namespace InstrumentService.Business.Abstractions;

public interface IInstrumentResponseModelFactory
{
    Task<InstrumentResponseModel> CreateAsync(Instrument instrument, CancellationToken cancellationToken);
}