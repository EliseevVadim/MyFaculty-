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
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(teacher => teacher.Id);
            builder.Property(teacher => teacher.Id).ValueGeneratedOnAdd();
            builder.HasIndex(teacher => teacher.VerifiactionToken).IsUnique();
            builder.HasOne(teacher => teacher.ScienceRank)
                .WithMany(rank => rank.Teachers)
                .HasForeignKey(teacher => teacher.ScienceRankId);
            builder.HasMany(teacher => teacher.Auditoriums)
                .WithOne(auditorium => auditorium.Teacher)
                .HasForeignKey(auditorium => auditorium.TeacherId);
            builder.HasMany(teacher => teacher.Pairs)
                .WithOne(pair => pair.Teacher)
                .HasForeignKey(pair => pair.TeacherId);
            builder.HasMany(teacher => teacher.TeacherDisciplines)
                .WithOne(teacherDiscipline => teacherDiscipline.Teacher)
                .HasForeignKey(teacherDiscipline => teacherDiscipline.TeacherId);
        }
    }
}
