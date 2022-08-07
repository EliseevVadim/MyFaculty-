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
    public class PairRepeatingConfiguration : IEntityTypeConfiguration<PairRepeating>
    {
        public void Configure(EntityTypeBuilder<PairRepeating> builder)
        {
            builder.HasKey(repeating => repeating.Id);
            builder.Property(repeating => repeating.Id).ValueGeneratedOnAdd();
            builder.HasMany(repeating => repeating.Pairs)
                .WithOne(pair => pair.PairRepeating)
                .HasForeignKey(pair => pair.PairRepeatingId);
            builder.HasData(new PairRepeating[]
            {
                new PairRepeating
                {
                    Id = 1,
                    RepeatingName = "Каждую неделю",
                    Created = DateTime.Now
                },
                new PairRepeating
                {
                    Id = 2,
                    RepeatingName = "По верхней неделе",
                    Created = DateTime.Now
                },
                new PairRepeating
                {
                    Id = 3,
                    RepeatingName = "По нижней неделе",
                    Created = DateTime.Now
                }
            });
        }
    }
}
