using AutoMapper;
using InstrumentService.Business.Abstractions;
using InstrumentService.Business.Models;
using InstrumentService.Business.Models.Response;
using InstrumentService.DataAccess.Constants;
using InstrumentService.DataAccess.Entities;

namespace InstrumentService.Business.Strategies;

public class PianoResponseStrategy(IMapper mapper, IInstrumentFormMetadataService instrumentFormMetadataService)
    : IInstrumentResponseStrategy
{
    public bool CanHandle(Instrument instrument) => instrument.Type.ToLower().Equals(InstrumentTypes.Piano.ToLower());

    public async Task<InstrumentResponseModel> HandleAsync(Instrument instrument,
        CancellationToken cancellationToken)
    {
        var instrumentResponseModel = mapper.Map<InstrumentResponseModel>(instrument);
        var piano = (Piano)instrument;
        var fieldLabels = await instrumentFormMetadataService
            .GetFieldLabelsForSpecificTypeAsync(instrument.Type, cancellationToken);

        var properties = new List<PropertyModel>();

        foreach (var fieldLabel in fieldLabels)
        {
            object value = fieldLabel.Name switch
            {
                nameof(Piano.ActionType) => piano.ActionType,
                nameof(Piano.CaseWood) => piano.CaseWood,
                nameof(Piano.Weight) => piano.Weight,
                nameof(Piano.NumberOfKeys) => piano.NumberOfKeys,
                nameof(Piano.NumberOfPedals) => piano.NumberOfPedals,
                _ => throw new ArgumentOutOfRangeException()
            };
            var property = new PropertyModel(fieldLabel.Label, value);
            properties.Add(property);
        }

        instrumentResponseModel.Properties.AddRange(properties);

        return instrumentResponseModel;
    }
}