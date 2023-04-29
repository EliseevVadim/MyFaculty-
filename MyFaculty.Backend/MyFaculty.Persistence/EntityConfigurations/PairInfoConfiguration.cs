using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class PairInfoConfiguration : IEntityTypeConfiguration<PairInfo>
    {
        public void Configure(EntityTypeBuilder<PairInfo> builder)
        {
            builder.HasKey(info => info.Id);
            builder.Property(info => info.Id).ValueGeneratedOnAdd();
            builder.HasMany(info => info.Pairs)
                .WithOne(pair => pair.PairInfo)
                .HasForeignKey(pair => pair.PairInfoId);
        }
    }
}
