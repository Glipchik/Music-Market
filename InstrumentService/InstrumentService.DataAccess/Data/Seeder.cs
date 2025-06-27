using InstrumentService.DataAccess.Abstractions;
using InstrumentService.DataAccess.Entities;
using InstrumentType = InstrumentService.DataAccess.Entities.InstrumentType;

namespace InstrumentService.DataAccess.Data;

public class Seeder(
    IInstrumentFormMetadataRepository instrumentFormMetadataRepository,
    IInstrumentTypeRepository instrumentTypeRepository)
{
    public async Task SeedInstrumentTypes()
    {
        var instrumentTypes = new List<InstrumentType>
        {
            new()
            {
                Id = "guitar",
                Label = "Guitar",
                IconPath = "http://localhost:9000/instrument-photos/icons/guitar.svg"
            },
            new()
            {
                Id = "piano",
                Label = "Piano",
                IconPath = "http://localhost:9000/instrument-photos/icons/piano.svg"
            },
            new()
            {
                Id = "drum",
                Label = "Drums",
                IconPath = "http://localhost:9000/instrument-photos/icons/drums.svg"
            },
            new()
            {
                Id = "cello",
                Label = "Cello",
                IconPath = "http://localhost:9000/instrument-photos/icons/cello.svg"
            }
        };
        await instrumentTypeRepository.UpsertAsync(instrumentTypes, CancellationToken.None);
    }

    public async Task SeedBaseMetadataAsync()
    {
        var baseMetadata = new InstrumentFormMetadata
        {
            Id = "base",
            Fields =
            [
                new FormFieldDescriptor
                {
                    Name = "Name",
                    Label = "Name",
                    Type = "text",
                    IsRequired = true,
                    DefaultValue = null,
                    Options = null,
                    Placeholder = "Instrument name"
                },
                new FormFieldDescriptor
                {
                    Name = "Manufacturer",
                    Label = "Manufacturer",
                    Type = "text",
                    IsRequired = true,
                    DefaultValue = null,
                    Options = null,
                    Placeholder = "Enter manufacturer"
                },
                new FormFieldDescriptor
                {
                    Name = "Price",
                    Label = "Price",
                    Type = "currency",
                    IsRequired = true,
                    DefaultValue = null,
                    Options = null,
                    Placeholder = "Enter price"
                },
                new FormFieldDescriptor
                {
                    Name = "Description",
                    Label = "Description",
                    Type = "textarea",
                    IsRequired = false,
                    DefaultValue = null,
                    Options = null,
                    Placeholder = "Enter a detailed description of the instrument..."
                }
            ]
        };
        await instrumentFormMetadataRepository.UpsertAsync(baseMetadata, CancellationToken.None);
    }

    public async Task SeedGuitarMetadataAsync()
    {
        var guitarSpecificMetadata = new InstrumentFormMetadata
        {
            Id = "guitar",
            Fields =
            [
                new FormFieldDescriptor
                {
                    Name = "NumberOfStrings",
                    Label = "Number of Strings",
                    Type = "select",
                    IsRequired = true,
                    DefaultValue = null,
                    Options = ["4", "6", "7", "8", "12"],
                    Placeholder = "Enter the number of strings"
                },
                new FormFieldDescriptor
                {
                    Name = "TopWood",
                    Label = "Top Wood",
                    Type = "text",
                    IsRequired = true,
                    DefaultValue = null,
                    Options = null,
                    Placeholder = "Enter top wood material"
                },
                new FormFieldDescriptor
                {
                    Name = "BodyWood",
                    Label = "Body Wood",
                    Type = "text",
                    IsRequired = true,
                    DefaultValue = null,
                    Placeholder = "Enter body wood material"
                },
                new FormFieldDescriptor
                {
                    Name = "HandOrientation",
                    Label = "Hand Orientation",
                    Type = "select",
                    IsRequired = true,
                    DefaultValue = "Right-Handed",
                    Options = ["Right-Handed", "Left-Handed"],
                    Placeholder = "Select hand orientation"
                },
                new FormFieldDescriptor
                {
                    Name = "BodyShape",
                    Label = "Body Shape",
                    Type = "select",
                    IsRequired = true,
                    DefaultValue = "Dreadnought",
                    Options =
                    [
                        "Dreadnought",
                        "Grand Auditorium",
                        "Concert",
                        "Grand Concert",
                        "Jumbo",
                        "Parlor",
                        "Auditorium",
                        "Classical"
                    ],
                    Placeholder = "Select body shape"
                },
                new FormFieldDescriptor
                {
                    Name = "NutWidth",
                    Label = "Nut Width",
                    Type = "number",
                    IsRequired = false,
                    DefaultValue = null,
                    Options = null,
                    Placeholder = "Enter nut width"
                }
            ]
        };
        await instrumentFormMetadataRepository.UpsertAsync(guitarSpecificMetadata, CancellationToken.None);
    }

    public async Task SeedPianoMetadataAsync()
    {
        var pianoSpecificMetadata = new InstrumentFormMetadata
        {
            Id = "piano",
            Fields =
            [
                new FormFieldDescriptor
                {
                    Name = "ActionType",
                    Label = "Action Type",
                    Type = "select",
                    IsRequired = true,
                    DefaultValue = null,
                    Options = ["Hammer", "Upright", "Grand", "Digital"],
                    Placeholder = "Select action type"
                },
                new FormFieldDescriptor
                {
                    Name = "CaseWood",
                    Label = "Case Wood Material",
                    Type = "text",
                    IsRequired = false,
                    DefaultValue = null,
                    Options = null,
                    Placeholder = "Enter case wood, e.g., Mahogany, Walnut, Birch"
                },
                new FormFieldDescriptor
                {
                    Name = "Weight",
                    Label = "Weight",
                    Type = "number",
                    IsRequired = true,
                    DefaultValue = null,
                    Options = null,
                    Placeholder = "Enter weight in kilograms"
                },
                new FormFieldDescriptor
                {
                    Name = "NumberOfKeys",
                    Label = "Number of Keys",
                    Type = "select",
                    IsRequired = true,
                    DefaultValue = "88",
                    Options = ["61", "73", "76", "88"],
                    Placeholder = "Select number of keys"
                },
                new FormFieldDescriptor
                {
                    Name = "NumberOfPedals",
                    Label = "Number of Pedals",
                    Type = "select",
                    IsRequired = true,
                    DefaultValue = "3",
                    Options = ["2", "3"],
                    Placeholder = "Select number of pedals"
                }
            ]
        };
        await instrumentFormMetadataRepository.UpsertAsync(pianoSpecificMetadata, CancellationToken.None);
    }

    public async Task SeedCelloMetadataAsync()
    {
        var celloSpecificMetadata = new InstrumentFormMetadata
        {
            Id = "cello",
            Fields =
            [
                new FormFieldDescriptor
                {
                    Name = "Size",
                    Label = "Size",
                    Type = "select",
                    IsRequired = true,
                    DefaultValue = "4/4",
                    Options = ["1/16", "1/10", "1/8", "1/4", "1/2", "3/4", "7/8", "4/4"],
                    Placeholder = "Select cello size"
                },
                new FormFieldDescriptor
                {
                    Name = "TopWood",
                    Label = "Top Wood Material",
                    Type = "text",
                    IsRequired = true,
                    DefaultValue = null,
                    Placeholder = "Enter top wood, e.g., Spruce, European Spruce"
                },
                new FormFieldDescriptor
                {
                    Name = "EndpinType",
                    Label = "Endpin Type",
                    Type = "select",
                    IsRequired = true,
                    DefaultValue = "Carbon Fiber",
                    Options = ["Carbon Fiber", "Wood", "Steel"],
                    Placeholder = "Select endpin type"
                },
                new FormFieldDescriptor
                {
                    Name = "VarnishType",
                    Label = "Varnish Type",
                    Type = "text",
                    IsRequired = false,
                    DefaultValue = null,
                    Placeholder = "e.g., Oil Varnish, Spirit Varnish, Antique Finish"
                }
            ]
        };
        await instrumentFormMetadataRepository.UpsertAsync(celloSpecificMetadata, CancellationToken.None);
    }

    public async Task SeedDrumMetadataAsync()
    {
        var drumSpecificMetadata = new InstrumentFormMetadata
        {
            Id = "drum",
            Fields =
            [
                new FormFieldDescriptor
                {
                    Name = "NumberOfPieces",
                    Label = "Number of Pieces",
                    Type = "number",
                    IsRequired = true,
                    DefaultValue = null,
                    Placeholder = "Enter the number of pieces"
                },
                new FormFieldDescriptor
                {
                    Name = "ShellMaterial",
                    Label = "Shell Material",
                    Type = "select",
                    IsRequired = true,
                    DefaultValue = "Maple",
                    Options = ["Maple", "Birch", "Mahogany", "Poplar", "Oak", "Acrylic"],
                    Placeholder = "Select shell material"
                },
                new FormFieldDescriptor
                {
                    Name = "Configuration",
                    Label = "Configuration",
                    Type = "select",
                    IsRequired = true,
                    DefaultValue = "Standard Kit",
                    Options =
                    [
                        "Standard Kit", "Fusion Kit", "Bop Kit", "Shell Pack", "Snare Drum", "Bass Drum", "Tom",
                        "Floor Tom"
                    ],
                    Placeholder = "Select drum configuration"
                },
                new FormFieldDescriptor
                {
                    Name = "CymbalsIncluded",
                    Label = "Cymbals Included",
                    Type = "checkbox",
                    IsRequired = false,
                    DefaultValue = false
                }
            ]
        };
        await instrumentFormMetadataRepository.UpsertAsync(drumSpecificMetadata, CancellationToken.None);
    }
}