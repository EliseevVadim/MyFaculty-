using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
