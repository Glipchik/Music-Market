using AnalyticsService.Business.Abstractions;
using AnalyticsService.Business.Entities;
using AnalyticsService.DataAccess.Data;

namespace AnalyticsService.DataAccess.Repositories;

public class UserStatRepository(ApplicationDbContext context) : Repository<UserStat>(context), IUserStatRepository;