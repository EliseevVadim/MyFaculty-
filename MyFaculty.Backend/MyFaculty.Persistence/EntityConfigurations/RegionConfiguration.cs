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
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasKey(region => region.Id);
            builder.Property(region => region.Id).ValueGeneratedOnAdd();
            builder.Property(region => region.RegionName).IsRequired();
            builder.HasIndex(region => region.RegionName).IsUnique();
            builder.HasMany(region => region.Cities)
                .WithOne(city => city.Region)
                .HasForeignKey(city => city.RegionId);
            builder.HasOne(region => region.Country)
                .WithMany(country => country.Regions)
                .HasForeignKey(region => region.CountryId);
        }
    }
}
