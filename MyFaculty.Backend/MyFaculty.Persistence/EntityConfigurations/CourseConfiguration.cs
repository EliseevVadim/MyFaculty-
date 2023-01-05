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
            builder.Property(course => course.FacultyId)
                .IsRequired(false)
                .HasDefaultValue(null);
            builder.HasMany(course => course.Groups)
                .WithOne(group => group.Course)
                .HasForeignKey(group => group.CourseId);
            builder.HasOne(course => course.Faculty)
                .WithMany(faculty => faculty.Courses)
                .HasForeignKey(course => course.FacultyId);
            builder.HasMany(course => course.Students)
                .WithOne(user => user.Course)
                .HasForeignKey(user => user.CourseId);
        }
    }
}
