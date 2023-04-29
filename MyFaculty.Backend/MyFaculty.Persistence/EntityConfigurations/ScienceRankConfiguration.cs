using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class ScienceRankConfiguration : IEntityTypeConfiguration<ScienceRank>
    {
        public void Configure(EntityTypeBuilder<ScienceRank> builder)
        {
            builder.HasKey(rank => rank.Id);
            builder.Property(rank => rank.Id).ValueGeneratedOnAdd();
            builder.HasMany(rank => rank.Teachers)
                .WithOne(teacher => teacher.ScienceRank)
                .HasForeignKey(teacher => teacher.ScienceRankId);
        }
    }
}
