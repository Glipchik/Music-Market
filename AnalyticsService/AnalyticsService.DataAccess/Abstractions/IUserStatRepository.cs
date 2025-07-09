using AnalyticsService.DataAccess.Entities;

namespace AnalyticsService.DataAccess.Abstractions;

public interface IUserStatRepository : IRepository<UserStat, string>;