using AutoMapper;
using InstrumentService.Business.Abstractions;
using InstrumentService.Business.Models;
using InstrumentService.Business.Models.Response;
using InstrumentService.DataAccess.Constants;
using InstrumentService.DataAccess.Entities;

namespace InstrumentService.Business.Strategies;

public class CelloResponseStrategy(
    IMapper mapper,
    IInstrumentFormMetadataService instrumentFormMetadataService)
    : IInstrumentResponseStrategy
{
    public bool CanHandle(Instrument instrument) => instrument.Type.ToLower().Equals(InstrumentTypes.Cello.ToLower());

    public async Task<InstrumentResponseModel> HandleAsync(Instrument instrument,
        CancellationToken cancellationToken)
    {
        var instrumentResponseModel = mapper.Map<InstrumentResponseModel>(instrument);
        var cello = (Cello)instrument;
        var fieldLabels = await instrumentFormMetadataService
            .GetFieldLabelsForSpecificTypeAsync(instrument.Type, cancellationToken);

        var properties = new List<PropertyModel>();

        foreach (var fieldLabel in fieldLabels)
        {
            object value = fieldLabel.Name switch
            {
                nameof(Cello.Size) => cello.Size,
                nameof(Cello.TopWood) => cello.TopWood,
                nameof(Cello.EndpinType) => cello.EndpinType,
                nameof(Cello.VarnishType) => cello.VarnishType,
                _ => throw new ArgumentOutOfRangeException()
            };
            var property = new PropertyModel(fieldLabel.Label, value);
            properties.Add(property);
        }

        instrumentResponseModel.Properties.AddRange(properties);

        return instrumentResponseModel;
    }
}