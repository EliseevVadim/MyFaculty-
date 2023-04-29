using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class ExpertSystemStateConfiguration : IEntityTypeConfiguration<ExpertSystemState>
    {
        public void Configure(EntityTypeBuilder<ExpertSystemState> builder)
        {
            builder.HasKey(state => state.Id);
            builder.Property(state => state.Id).ValueGeneratedOnAdd();
            builder.Property(state => state.Question).IsRequired();
            builder.Property(state => state.Explanation).IsRequired();
            builder.HasMany(state => state.Answers)
                .WithOne(answer => answer.ExpertSystemState)
                .HasForeignKey(answer => answer.StateId);
        }
    }
}
