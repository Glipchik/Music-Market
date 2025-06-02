using AnalyticsService.Business.Entities;
using AnalyticsService.Business.Models;
using AutoMapper;

namespace AnalyticsService.Business.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<InstrumentStat, InstrumentStatResult>();
        CreateMap<InstrumentDailyStat, InstrumentDailyStatResult>();
        CreateMap<UserStat, UserStatResult>();
    }
}