using AnalyticsService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyticsService.DataAccess.EntityTypeConfigurations;

public class InstrumentsDailyStatEntityTypeConfiguration : IEntityTypeConfiguration<InstrumentDailyStat>
{
    public void Configure(EntityTypeBuilder<InstrumentDailyStat> builder)
    {
        builder.HasKey(instrumentDailyStat =>
            new { instrumentDailyStat.InstrumentId, instrumentDailyStat.Date });

        builder.Property(instrumentDailyStat => instrumentDailyStat.InstrumentId).HasMaxLength(128).IsRequired();
    }
}