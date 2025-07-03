using AutoMapper;
using InstrumentService.Business.Abstractions;
using InstrumentService.Business.Models;
using InstrumentService.Business.Models.Response;
using InstrumentService.DataAccess.Constants;
using InstrumentService.DataAccess.Entities;

namespace InstrumentService.Business.Strategies;

public class GuitarResponseStrategy(IMapper mapper, IInstrumentFormMetadataService instrumentFormMetadataService)
    : IInstrumentResponseStrategy
{
    public bool CanHandle(Instrument instrument) => instrument.Type.ToLower().Equals(InstrumentTypes.Guitar.ToLower());

    public async Task<InstrumentResponseModel> HandleAsync(Instrument instrument,
        CancellationToken cancellationToken)
    {
        var instrumentResponseModel = mapper.Map<InstrumentResponseModel>(instrument);
        var guitar = (Guitar)instrument;
        var fieldLabels = await instrumentFormMetadataService
            .GetFieldLabelsForSpecificTypeAsync(instrument.Type, cancellationToken);

        var properties = new List<PropertyModel>();

        foreach (var fieldLabel in fieldLabels)
        {
            object value = fieldLabel.Name switch
            {
                nameof(Guitar.NumberOfStrings) => guitar.NumberOfStrings,
                nameof(Guitar.TopWood) => guitar.TopWood,
                nameof(Guitar.BodyWood) => guitar.BodyWood,
                nameof(Guitar.HandOrientation) => guitar.HandOrientation,
                nameof(Guitar.BodyShape) => guitar.BodyShape,
                nameof(Guitar.NutWidth) => guitar.NutWidth,
                _ => throw new ArgumentOutOfRangeException()
            };
            var property = new PropertyModel(fieldLabel.Label, value);
            properties.Add(property);
        }

        instrumentResponseModel.Properties.AddRange(properties);

        return instrumentResponseModel;
    }
}