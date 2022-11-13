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
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(country => country.Id);
            builder.Property(country => country.Id).ValueGeneratedOnAdd();
            builder.Property(country => country.CountryName).IsRequired();
            builder.HasIndex(country => country.CountryName).IsUnique();
            builder.HasMany(country => country.Regions)
                .WithOne(region => region.Country)
                .HasForeignKey(region => region.CountryId);
        }
    }
}
