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
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(group => group.Id);
            builder.Property(group => group.Id).ValueGeneratedOnAdd();
            builder.HasMany(group => group.Pairs)
                .WithOne(pair => pair.Group)
                .HasForeignKey(pair => pair.GroupId);
            builder.HasOne(group => group.Course)
                .WithMany(course => course.Groups)
                .HasForeignKey(group => group.CourseId);
        }
    }
}
