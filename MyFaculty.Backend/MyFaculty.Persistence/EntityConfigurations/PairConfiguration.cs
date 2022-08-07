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
    public class PairConfiguration : IEntityTypeConfiguration<Pair>
    {
        public void Configure(EntityTypeBuilder<Pair> builder)
        {
            builder.HasKey(pair => pair.Id);
            builder.Property(pair => pair.Id).ValueGeneratedOnAdd();
            builder.HasOne(pair => pair.Teacher)
                .WithMany(teacher => teacher.Pairs)
                .HasForeignKey(pair => pair.TeacherId);
            builder.HasOne(pair => pair.PairInfo)
                .WithMany(pairInfo => pairInfo.Pairs)
                .HasForeignKey(pair => pair.PairInfoId);
            builder.HasOne(pair => pair.Auditorium)
                .WithMany(auditorium => auditorium.Pairs)
                .HasForeignKey(pair => pair.AuditriumId);
            builder.HasOne(pair => pair.Discipline)
                .WithMany(discipline => discipline.Pairs)
                .HasForeignKey(pair => pair.DisciplineId);
            builder.HasOne(pair => pair.DayOfWeek)
                .WithMany(day => day.Pairs)
                .HasForeignKey(pair => pair.DayOfWeekId);
            builder.HasOne(pair => pair.PairRepeating)
                .WithMany(repeating => repeating.Pairs)
                .HasForeignKey(pair => pair.PairRepeatingId);
            builder.HasOne(pair => pair.Group)
                .WithMany(group => group.Pairs)
                .HasForeignKey(pair => pair.GroupId);
        }
    }
}
