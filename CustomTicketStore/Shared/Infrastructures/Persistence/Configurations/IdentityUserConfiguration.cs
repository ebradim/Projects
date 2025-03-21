using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace CustomTicketStore.Shared.Infrastructures.Persistence.Configurations;

internal sealed class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUser<int>> builder)
    {
        builder.HasIndex(e => e.NormalizedEmail).IsUnique().HasFilter($@"""{nameof(IdentityUser<int>.NormalizedEmail)}"" IS NOT NULL");

        builder.Property(e => e.Email).IsRequired(false).HasMaxLength(330);
        builder.Property(e => e.NormalizedEmail).IsRequired(false).HasMaxLength(330);

        builder.Property(e => e.UserName).IsRequired(true).HasMaxLength(26);
        builder.Property(e => e.NormalizedUserName).IsRequired(true).HasMaxLength(26);

        builder.Property(e => e.PhoneNumber).IsRequired(false).HasMaxLength(20);






        builder.ToTable("Users");

    }
}
