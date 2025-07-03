using InstrumentService.Business.Abstractions;
using InstrumentService.Business.Common;
using InstrumentService.Business.Models.Response;
using InstrumentService.DataAccess.Entities;

namespace InstrumentService.Business.Factories;

public class InstrumentResponseModelFactory(IEnumerable<IInstrumentResponseStrategy> strategies)
    : IInstrumentResponseModelFactory
{
    public Task<InstrumentResponseModel> CreateAsync(Instrument instrument, CancellationToken cancellationToken)
    {
        try
        {
            var instrumentResponseStrategy = strategies.Single(strategy => strategy.CanHandle(instrument));
            return instrumentResponseStrategy.HandleAsync(instrument, cancellationToken);
        }
        catch (InvalidOperationException exception)
        {
            throw new InvalidOperationException(
                ErrorMessages.FailedToResolveInstrumentStrategy(instrument.Id, instrument.Type), exception);
        }
    }
}