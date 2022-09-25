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
    public class ExpertSystemAnswerConfiguration : IEntityTypeConfiguration<ExpertSystemAnswer>
    {
        public void Configure(EntityTypeBuilder<ExpertSystemAnswer> builder)
        {
            builder.HasKey(answer => answer.Id);
            builder.Property(answer => answer.Id).ValueGeneratedOnAdd();
            builder.Property(answer => answer.Text).IsRequired();
            builder.Property(answer => answer.StateTransitionId).IsRequired();
            builder.Property(answer => answer.StateId).IsRequired();
            builder.HasOne(answer => answer.ExpertSystemStateTransition)
                .WithOne(transition => transition.Answer)
                .HasForeignKey<ExpertSystemAnswer>(answer => answer.StateTransitionId);
            builder.HasOne(answer => answer.ExpertSystemState)
                .WithMany(state => state.Answers)
                .HasForeignKey(answer => answer.StateId);
        }
    }
}
