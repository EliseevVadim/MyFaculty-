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
    public class ClubTaskConfiguration : IEntityTypeConfiguration<ClubTask>
    {
        public void Configure(EntityTypeBuilder<ClubTask> builder)
        {
            builder.ToTable("clubTasks");
            builder.Property(task => task.DeadLine).IsRequired();
            builder.Property(task => task.Cost).IsRequired();
            builder.HasOne(task => task.OwningStudyClub)
                .WithMany(club => club.ClubTasks)
                .HasForeignKey(task => task.StudyClubId);
        }
    }
}
