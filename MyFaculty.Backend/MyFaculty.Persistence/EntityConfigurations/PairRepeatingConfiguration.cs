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
            builder.HasIndex(repeating => repeating.Id).IsUnique();
            builder.HasMany(repeating => repeating.Pairs)
                .WithOne(pair => pair.PairRepeating)
                .HasForeignKey(pair => pair.PairRepeatingId);
        }
    }
}
