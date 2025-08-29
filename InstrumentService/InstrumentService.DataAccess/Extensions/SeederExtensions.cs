using InstrumentService.DataAccess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace InstrumentService.DataAccess.Extensions;

public static class SeederExtensions
{
    public static async Task SeedInstrumentsAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();

        await seeder.SeedBaseMetadataAsync();
        await seeder.SeedPianoMetadataAsync();
        await seeder.SeedGuitarMetadataAsync();
        await seeder.SeedCelloMetadataAsync();
        await seeder.SeedDrumMetadataAsync();
        await seeder.SeedInstrumentTypes();
    }
}