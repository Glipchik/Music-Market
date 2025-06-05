namespace Shared.Messaging.Contracts.Events.Instrument;

public record InstrumentViewed(Guid InstrumentId, DateOnly Date);