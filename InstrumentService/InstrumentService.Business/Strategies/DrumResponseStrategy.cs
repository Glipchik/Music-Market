using AutoMapper;
using InstrumentService.Business.Abstractions;
using InstrumentService.Business.Models;
using InstrumentService.Business.Models.Response;
using InstrumentService.DataAccess.Constants;
using InstrumentService.DataAccess.Entities;

namespace InstrumentService.Business.Strategies;

public class DrumResponseStrategy(
    IMapper mapper,
    IInstrumentFormMetadataService instrumentFormMetadataService)
    : IInstrumentResponseStrategy
{
    public bool CanHandle(Instrument instrument) => instrument.Type.ToLower().Equals(InstrumentTypes.Drum.ToLower());

    public async Task<InstrumentResponseModel> HandleAsync(Instrument instrument,
        CancellationToken cancellationToken)
    {
        var instrumentResponseModel = mapper.Map<InstrumentResponseModel>(instrument);
        var drum = (Drum)instrument;
        var fieldLabels = await instrumentFormMetadataService
            .GetFieldLabelsForSpecificTypeAsync(instrument.Type, cancellationToken);

        var properties = new List<PropertyModel>();

        foreach (var fieldLabel in fieldLabels)
        {
            object value = fieldLabel.Name switch
            {
                nameof(Drum.NumberOfPieces) => drum.NumberOfPieces,
                nameof(Drum.ShellMaterial) => drum.ShellMaterial,
                nameof(Drum.Configuration) => drum.Configuration,
                nameof(Drum.CymbalsIncluded) => drum.CymbalsIncluded,
                _ => throw new ArgumentOutOfRangeException()
            };
            var property = new PropertyModel(fieldLabel.Label, value);
            properties.Add(property);
        }

        instrumentResponseModel.Properties.AddRange(properties);

        return instrumentResponseModel;
    }
}