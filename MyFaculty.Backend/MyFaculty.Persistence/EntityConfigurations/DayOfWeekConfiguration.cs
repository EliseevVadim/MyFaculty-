using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Threading.Tasks;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class WorkDayOfWeekConfiguration : IEntityTypeConfiguration<WorkDayOfWeek>
    {
        public void Configure(EntityTypeBuilder<WorkDayOfWeek> builder)
        {
            builder.HasKey(day => day.Id);
            builder.Property(day => day.Id).ValueGeneratedOnAdd();
            builder.HasMany(day => day.Pairs)
                .WithOne(pair => pair.DayOfWeek)
                .HasForeignKey(pair => pair.DayOfWeekId);
            builder.HasData(new WorkDayOfWeek[]
            {
                new WorkDayOfWeek
                {
                    Id = 1,
                    DaysName = "Понедельник",
                    Created = DateTime.Now
                },
                new WorkDayOfWeek
                {
                    Id = 2,
                    DaysName = "Вторник",
                    Created = DateTime.Now
                },
                new WorkDayOfWeek
                {
                    Id = 3,
                    DaysName = "Среда",
                    Created = DateTime.Now
                },
                new WorkDayOfWeek
                {
                    Id = 4,
                    DaysName = "Четверг",
                    Created = DateTime.Now
                },
                new WorkDayOfWeek
                {
                    Id = 5,
                    DaysName = "Пятница",
                    Created = DateTime.Now
                },
                new WorkDayOfWeek
                {
                    Id = 6,
                    DaysName = "Суббота",
                    Created = DateTime.Now
                }
            });
        }
    }
}
