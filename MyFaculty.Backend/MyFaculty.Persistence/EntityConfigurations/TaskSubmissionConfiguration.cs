using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;
using MyFaculty.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class TaskSubmissionConfiguration : IEntityTypeConfiguration<TaskSubmission>
    {
        public void Configure(EntityTypeBuilder<TaskSubmission> builder)
        {
            builder.HasKey(submission => submission.Id);
            builder.Property(submission => submission.Id).ValueGeneratedOnAdd();
            builder.Property(submission => submission.TextContent).IsRequired(false);
            builder.Property(submission => submission.Attachments).IsRequired(false);
            builder.Property(submission => submission.SubmissionAttachmentsUid).IsRequired();
            builder.Property(submission => submission.Status).HasDefaultValue(TaskSubmissionStatus.PendingSubmission);
            builder.Property(submission => submission.Status)
                .HasConversion(
                    value => value.ToString(),
                    value => (TaskSubmissionStatus)Enum.Parse(typeof(TaskSubmissionStatus), value)
                );
            builder.HasOne(submission => submission.ClubTask)
                .WithMany(task => task.Submissions)
                .HasForeignKey(submission => submission.ClubTaskId);
            builder.HasOne(submission => submission.Author)
                .WithMany(user => user.TaskSubmissions)
                .HasForeignKey(submission => submission.AuthorId);
            builder.HasOne(submission => submission.Review)
                .WithOne(review => review.TaskSubmission)
                .HasForeignKey<TaskSubmissionReview>(review => review.SubmissionId);
        }
    }
}
