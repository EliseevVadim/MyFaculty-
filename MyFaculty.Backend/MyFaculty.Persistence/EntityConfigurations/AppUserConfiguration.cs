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
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("users");
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Email).IsRequired();
            builder.HasIndex(user => user.Email).IsUnique();
            builder.Property(user => user.AvatarPath).IsRequired(false);
            builder.Property(user => user.BirthDate).IsRequired(false);
            builder.Property(user => user.CityId).IsRequired(false);
            builder.HasOne(user => user.City)
                .WithMany(city => city.Users)
                .HasForeignKey(user => user.CityId);
            builder.Property(user => user.Website).IsRequired(false);
            builder.Property(user => user.VKLink).IsRequired(false);
            builder.Property(user => user.TelegramLink).IsRequired(false);
        }
    }
}
