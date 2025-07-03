using InstrumentService.Business.Models.Response;
using InstrumentService.DataAccess.Entities;

namespace InstrumentService.Business.Abstractions;

public interface IInstrumentResponseStrategy
{
    bool CanHandle(Instrument instrument);
    Task<InstrumentResponseModel> HandleAsync(Instrument instrument, CancellationToken cancellationToken);
}