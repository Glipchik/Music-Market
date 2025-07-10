using AnalyticsService.Business.Models;
using AnalyticsService.DataAccess.Entities;
using AutoMapper;

namespace AnalyticsService.Business.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<InstrumentStat, InstrumentStatResult>();
        CreateMap<InstrumentDailyStat, InstrumentDailyStatResult>();
        CreateMap<UserStat, UserStatResult>();
        CreateMap<InstrumentStat, TopInstrumentModel>();
    }
}