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
    public class ScienceRankConfiguration : IEntityTypeConfiguration<ScienceRank>
    {
        public void Configure(EntityTypeBuilder<ScienceRank> builder)
        {
            builder.HasKey(rank => rank.Id);
            builder.HasIndex(rank => rank.Id).IsUnique();
            builder.HasMany(rank => rank.Teachers)
                .WithOne(teacher => teacher.ScienceRank)
                .HasForeignKey(teacher => teacher.ScienceRankId);
        }
    }
}
