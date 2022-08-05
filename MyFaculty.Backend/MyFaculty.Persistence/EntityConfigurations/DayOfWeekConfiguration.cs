using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class DayOfWeekConfiguration : IEntityTypeConfiguration<DayOfWeek>
    {
        public void Configure(EntityTypeBuilder<DayOfWeek> builder)
        {
            builder.HasKey(day => day.Id);
            builder.HasIndex(day => day.Id).IsUnique();
            builder.HasMany(day => day.Pairs)
                .WithOne(pair => pair.DayOfWeek)
                .HasForeignKey(pair => pair.DayOfWeekId);
        }
    }
}
