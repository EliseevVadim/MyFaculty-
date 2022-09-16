using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFaculty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class FloorConfiguration : IEntityTypeConfiguration<Floor>
    {
        public void Configure(EntityTypeBuilder<Floor> builder)
        {
            builder.HasKey(floor => floor.Id);
            builder.Property(floor => floor.Id).ValueGeneratedOnAdd();
            builder.Property(floor => floor.Name).IsRequired();
            builder.Property(floor => floor.Bounds).IsRequired();
            builder.Property(floor => floor.FacultyId)
                .IsRequired(false)
                .HasDefaultValue(null);
            builder.HasMany(floor => floor.Auditoriums)
                .WithOne(auditorium => auditorium.Floor)
                .HasForeignKey(auditorium => auditorium.FloorId);
            builder.HasMany(floor => floor.SecondaryObjects)
                .WithOne(secondaryObject => secondaryObject.Floor)
                .HasForeignKey(secondaryObject => secondaryObject.FloorId);
            builder.HasOne(floor => floor.Faculty)
                .WithMany(faculty => faculty.Floors)
                .HasForeignKey(floor => floor.FacultyId);
        }
    }
}
