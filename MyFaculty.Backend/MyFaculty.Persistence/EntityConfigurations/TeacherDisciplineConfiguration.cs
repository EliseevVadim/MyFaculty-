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
    public class TeacherDisciplineConfiguration : IEntityTypeConfiguration<TeacherDiscipline>
    {
        public void Configure(EntityTypeBuilder<TeacherDiscipline> builder)
        {
            builder.HasKey(teacherDiscipline => teacherDiscipline.Id);
            builder.Property(teacherDiscipline => teacherDiscipline.Id).ValueGeneratedOnAdd();
            builder.HasOne(teacherDiscipline => teacherDiscipline.Teacher)
                .WithMany(teacher => teacher.TeacherDisciplines)
                .HasForeignKey(teacherDiscipline => teacherDiscipline.TeacherId);
            builder.HasOne(teacherDiscipline => teacherDiscipline.Discipline)
                .WithMany(discipline => discipline.TeacherDisciplines)
                .HasForeignKey(teacherDiscipline => teacherDiscipline.DisciplineId);
        }
    }
}
