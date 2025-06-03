using AnalyticsService.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyticsService.DataAccess.EntityTypeConfigurations;

public class InstrumentStatEntityTypeConfiguration : IEntityTypeConfiguration<InstrumentStat>
{
    public void Configure(EntityTypeBuilder<InstrumentStat> builder)
    {
         builder.HasKey(e => e.InstrumentId);
    }
}