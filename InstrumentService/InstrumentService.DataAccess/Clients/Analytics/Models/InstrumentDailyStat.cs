namespace InstrumentService.DataAccess.Clients.Analytics.Models;

public record InstrumentDailyStat(string InstrumentId, DateOnly Date, int Views);