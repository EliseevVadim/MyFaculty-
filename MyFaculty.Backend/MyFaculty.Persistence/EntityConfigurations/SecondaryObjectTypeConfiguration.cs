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
    public class SecondaryObjectTypeConfiguration : IEntityTypeConfiguration<SecondaryObjectType>
    {
        public void Configure(EntityTypeBuilder<SecondaryObjectType> builder)
        {
            builder.HasKey(type => type.Id);
            builder.Property(type => type.Id).ValueGeneratedOnAdd();
            builder.HasIndex(type => type.ObjectTypeName).IsUnique();
            builder.HasIndex(type => type.TypePath).IsUnique();
            builder.HasMany(type => type.SecondaryObjects)
                .WithOne(secondaryObject => secondaryObject.SecondaryObjectType)
                .HasForeignKey(secondaryObject => secondaryObject.SecondaryObjectTypeId);
        }
    }
}
