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
    public class ExpertSystemStateTransitionConfiguration : IEntityTypeConfiguration<ExpertSystemStateTransition>
    {
        public void Configure(EntityTypeBuilder<ExpertSystemStateTransition> builder)
        {
            builder.HasKey(transition => transition.Id);
            builder.Property(transition => transition.Id).ValueGeneratedOnAdd();
            builder.Property(transition => transition.InitialStateId).IsRequired();
            builder.Property(transition => transition.FinalStateId).IsRequired();
            builder.Property(transition => transition.IsLast).IsRequired();
            builder.Property(transition => transition.AnswerId).IsRequired();
            builder.HasOne(transition => transition.InitialState)
                .WithMany(state => state.ExpertSystemInitialStateTransitions)
                .HasForeignKey(transition => transition.InitialStateId);
            builder.HasOne(transition => transition.FinalState)
                .WithMany(state => state.ExpertSystemFinalStateTransitions)
                .HasForeignKey(transition => transition.FinalStateId);
            builder.HasOne(transition => transition.Answer)
                .WithOne(answer => answer.ExpertSystemStateTransition)
                .HasForeignKey<ExpertSystemStateTransition>(transition => transition.AnswerId);
        }
    }
}
