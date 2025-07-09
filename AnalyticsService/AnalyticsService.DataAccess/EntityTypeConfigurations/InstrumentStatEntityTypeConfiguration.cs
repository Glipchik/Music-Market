using AnalyticsService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyticsService.DataAccess.EntityTypeConfigurations;

public class InstrumentStatEntityTypeConfiguration : IEntityTypeConfiguration<InstrumentStat>
{
    public void Configure(EntityTypeBuilder<InstrumentStat> builder)
    {
        builder.HasKey(instrumentStat => instrumentStat.InstrumentId);

        builder.Property(instrumentStat => instrumentStat.InstrumentId).HasMaxLength(128).IsRequired();
    }
}