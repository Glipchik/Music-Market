using AnalyticsService.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyticsService.DataAccess.EntityTypeConfigurations;

public class UserStatEntityTypeConfiguration : IEntityTypeConfiguration<UserStat>
{
    public void Configure(EntityTypeBuilder<UserStat> builder)
    {
        builder.HasKey(e => e.UserId);
    }
}
