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
    public class PairInfoConfiguration : IEntityTypeConfiguration<PairInfo>
    {
        public void Configure(EntityTypeBuilder<PairInfo> builder)
        {
            builder.HasKey(info => info.Id);
            builder.HasIndex(info => info.Id).IsUnique();
            builder.HasMany(info => info.Pairs)
                .WithOne(pair => pair.PairInfo)
                .HasForeignKey(pair => pair.PairInfoId);
        }
    }
}
