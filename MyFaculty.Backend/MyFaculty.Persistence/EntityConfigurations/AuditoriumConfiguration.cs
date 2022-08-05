using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class AuditoriumConfiguration : IEntityTypeConfiguration<Auditorium>
    {
        public void Configure(EntityTypeBuilder<Auditorium> builder)
        {
            builder.HasKey(auditorium => auditorium.Id);
            builder.HasIndex(auditorium => auditorium.Id).IsUnique();
            builder.HasOne(auditorium => auditorium.Teacher)
                .WithMany(teacher => teacher.Auditoriums)
                .HasForeignKey(auditorium => auditorium.TeacherId);
            builder.HasOne(auditorium => auditorium.Floor)
                .WithMany(floor => floor.Auditoriums)
                .HasForeignKey(auditorium => auditorium.FloorId);
        }
    }
}
