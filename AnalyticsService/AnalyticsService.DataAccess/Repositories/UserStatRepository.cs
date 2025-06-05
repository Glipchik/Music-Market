using AnalyticsService.DataAccess.Abstractions;
using AnalyticsService.DataAccess.Data;
using AnalyticsService.DataAccess.Entities;

namespace AnalyticsService.DataAccess.Repositories;

internal class UserStatRepository(ApplicationDbContext context) : Repository<UserStat>(context), IUserStatRepository;