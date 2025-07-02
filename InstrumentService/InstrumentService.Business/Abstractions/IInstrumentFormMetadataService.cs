using InstrumentService.Business.Models;
using InstrumentService.DataAccess.Entities;

namespace InstrumentService.Business.Abstractions;

public interface IInstrumentFormMetadataService
{
    Task<List<FormFieldDescriptor>> GetCombinedFormFieldDescriptorsAsync(string specificInstrumentType,
        CancellationToken cancellationToken);

    Task<List<FieldLabelModel>> GetFieldLabelsForSpecificTypeAsync(string specificInstrumentType,
        CancellationToken cancellationToken);
}