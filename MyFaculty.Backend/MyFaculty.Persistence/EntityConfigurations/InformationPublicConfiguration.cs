using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class InformationPublicConfiguration : IEntityTypeConfiguration<InformationPublic>
    {
        public void Configure(EntityTypeBuilder<InformationPublic> builder)
        {
            builder.HasKey(infoPublic => infoPublic.Id);
            builder.Property(infoPublic => infoPublic.Id).ValueGeneratedOnAdd();
            builder.Property(infoPublic => infoPublic.Description).IsRequired(false);
            builder.Property(infoPublic => infoPublic.ImagePath).IsRequired(false);
            builder.Property(infoPublic => infoPublic.PublicName).IsRequired();
            builder.Property(infoPublic => infoPublic.IsBanned).HasDefaultValue(false);
            builder.HasIndex(infoPublic => infoPublic.PublicName).IsUnique();
            builder.HasOne(infoPublic => infoPublic.Owner)
                .WithMany(user => user.OwnedInformationPublics)
                .HasForeignKey(infoPublic => infoPublic.OwnerId);
            builder.HasMany(infoPublic => infoPublic.Members)
                .WithMany(user => user.InformationPublics)
                .UsingEntity(j => j.ToTable("UsersAtInformationPublics"));
            builder.HasMany(infoPublic => infoPublic.BlockedUsers)
                .WithMany(user => user.BlockedPublics)
                .UsingEntity(j => j.ToTable("UsersBlockedInPublics"));
            builder.HasMany(infoPublic => infoPublic.Moderators)
                .WithMany(user => user.InformationPublicsAtModeration)
                .UsingEntity(j => j.ToTable("UsersModeratesInformationPublics"));
            builder.HasMany(infoPublic => infoPublic.AppliedBanReports)
                .WithOne(banReport => banReport.AffectedPublic)
                .HasForeignKey(banReport => banReport.PublicId);
        }
    }
}
