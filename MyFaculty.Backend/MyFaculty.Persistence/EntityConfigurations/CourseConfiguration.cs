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
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(course => course.Id);
            builder.Property(course => course.Id).ValueGeneratedOnAdd();
            builder.HasMany(course => course.Groups)
                .WithOne(group => group.Course)
                .HasForeignKey(group => group.CourseId);
        }
    }
}
