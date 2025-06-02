namespace AnalyticsService.Business.Models;

public record InstrumentStatResult(Guid InstrumentId, int Views, int ContactViews, int Bookmarks);