using AnalyticsService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyticsService.DataAccess.EntityTypeConfigurations;

public class UserStatEntityTypeConfiguration : IEntityTypeConfiguration<UserStat>
{
    public void Configure(EntityTypeBuilder<UserStat> builder)
    {
        builder.HasKey(userStat => userStat.UserId);

        builder.Property(userStat => userStat.UserId).HasMaxLength(128).IsRequired();
    }
}