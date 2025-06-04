using AnalyticsService.DataAccess.Abstractions;
using AnalyticsService.DataAccess.Data;
using AnalyticsService.DataAccess.Entities;

namespace AnalyticsService.DataAccess.Repositories;

public class UserStatRepository(ApplicationDbContext context) : Repository<UserStat>(context), IUserStatRepository;