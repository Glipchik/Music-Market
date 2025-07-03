using InstrumentService.Business.Abstractions;
using InstrumentService.Business.Common;
using InstrumentService.Business.Models;
using InstrumentService.DataAccess.Abstractions;
using InstrumentService.DataAccess.Constants;
using InstrumentService.DataAccess.Entities;
using Shared.Exceptions;

namespace InstrumentService.Business.Services;

public class InstrumentFormMetadataService(IInstrumentFormMetadataRepository instrumentFormMetadataRepository)
    : IInstrumentFormMetadataService
{
    public async Task<List<FormFieldDescriptor>> GetCombinedFormFieldDescriptorsAsync(string specificInstrumentType,
        CancellationToken cancellationToken)
    {
        var baseTask = instrumentFormMetadataRepository.GetByIdAsync(InstrumentTypes.Base, cancellationToken);
        var specificTask = instrumentFormMetadataRepository.GetByIdAsync(specificInstrumentType, cancellationToken);

        await Task.WhenAll(baseTask, specificTask);

        var baseMetadata = baseTask.Result;
        var specificMetadata = specificTask.Result;

        if (baseMetadata is null)
        {
            throw new NotFoundException(ErrorMessages.BaseFormMetadataNotFound);
        }

        if (specificMetadata is null)
        {
            throw new NotFoundException(ErrorMessages.SpecificFormMetadataNotFound(specificInstrumentType));
        }

        var fields = new List<FormFieldDescriptor>();

        fields.AddRange(baseMetadata.Fields);
        fields.AddRange(specificMetadata.Fields);

        return fields;
    }

    public async Task<List<FieldLabelModel>> GetFieldLabelsForSpecificTypeAsync(string specificInstrumentType,
        CancellationToken cancellationToken)
    {
        var specificMetadata = await instrumentFormMetadataRepository.GetByIdAsync(specificInstrumentType, cancellationToken);

        if (specificMetadata is null)
        {
            throw new NotFoundException(ErrorMessages.SpecificFormMetadataNotFound(specificInstrumentType));
        }
        
        var fields = specificMetadata.Fields;
        var fieldLabels = fields.Select(field => new FieldLabelModel(field.Name, field.Label)).ToList();

        return fieldLabels;
    }
}