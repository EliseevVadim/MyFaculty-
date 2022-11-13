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
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(city => city.Id);
            builder.Property(city => city.Id).ValueGeneratedOnAdd();
            builder.Property(city => city.CityName).IsRequired();
            builder.HasOne(city => city.Region)
                .WithMany(region => region.Cities)
                .HasForeignKey(city => city.RegionId);
            builder.HasMany(city => city.Users)
                .WithOne(user => user.City)
                .HasForeignKey(user => user.CityId);
        }
    }
}
