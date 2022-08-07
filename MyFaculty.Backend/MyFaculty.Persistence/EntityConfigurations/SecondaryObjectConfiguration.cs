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
    public class SecondaryObjectConfiguration : IEntityTypeConfiguration<SecondaryObject>
    {
        public void Configure(EntityTypeBuilder<SecondaryObject> builder)
        {
            builder.HasKey(secondaryObject => secondaryObject.Id);
            builder.Property(secondaryObject => secondaryObject.Id).ValueGeneratedOnAdd();
            builder.HasOne(secondaryObject => secondaryObject.Floor)
                .WithMany(floor => floor.SecondaryObjects)
                .HasForeignKey(secondaryObject => secondaryObject.FloorId);
            builder.HasOne(secondaryObject => secondaryObject.SecondaryObjectType)
                .WithMany(type => type.SecondaryObjects)
                .HasForeignKey(secondaryObject => secondaryObject.SecondaryObjectTypeId);
        }
    }
}
