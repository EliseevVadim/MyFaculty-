using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;

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
            builder.HasMany(task => task.Submissions)
                .WithOne(submission => submission.ClubTask)
                .HasForeignKey(submission => submission.ClubTaskId);
        }
    }
}
