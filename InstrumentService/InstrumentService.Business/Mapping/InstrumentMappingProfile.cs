using AutoMapper;
using InstrumentService.Business.Models;
using InstrumentService.Business.Models.Request;
using InstrumentService.Business.Models.Response;
using InstrumentService.DataAccess.Clients.Analytics.Models;
using InstrumentService.DataAccess.Clients.User.Models;
using InstrumentService.DataAccess.Entities;
using InstrumentService.DataAccess.Options;
using Microsoft.Extensions.Options;

namespace InstrumentService.Business.Mapping;

public class InstrumentMappingProfile : Profile
{
    public InstrumentMappingProfile()
    {
    }

    public InstrumentMappingProfile(IOptions<MinioOptions> minioOptions)
    {
        var minioOptionsValue = minioOptions.Value;

        CreateMap<InstrumentRequestModel, Instrument>()
            .Include<GuitarRequestModel, Guitar>()
            .Include<PianoRequestModel, Piano>()
            .Include<CelloRequestModel, Cello>()
            .Include<DrumRequestModel, Drum>();

        CreateMap<GuitarRequestModel, Guitar>();
        CreateMap<PianoRequestModel, Piano>();
        CreateMap<CelloRequestModel, Cello>();
        CreateMap<DrumRequestModel, Drum>();

        CreateMap<Instrument, InstrumentResponseModel>()
            .ForMember(dest => dest.PhotoUrls, opt =>
                opt.MapFrom(src =>
                    src.PhotoNames.Select(photoName =>
                        $"{minioOptionsValue.Host}/{minioOptionsValue.BucketName}/{photoName}").ToList()));

        CreateMap<InstrumentType, InstrumentTypeResponseModel>()
            .ForCtorParam("Value", opt =>
                opt.MapFrom(src => src.Id));

        CreateMap<FormFieldDescriptor, FormFieldDescriptorResponseModel>();

        CreateMap<InstrumentDailyStat, InstrumentDailyStatResponseModel>();
        CreateMap<InstrumentStat, InstrumentStatResponseModel>();

        CreateMap<InstrumentResponseModel, UserInstrumentResponseModel>();

        CreateMap<UserContacts, UserContactsModel>();

        CreateMap<TopInstrument, TopInstrumentModel>();
    }
}