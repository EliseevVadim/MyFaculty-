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
    public class UserBanReportConfiguration : IEntityTypeConfiguration<UserBanReport>
    {
        public void Configure(EntityTypeBuilder<UserBanReport> builder)
        {
            builder.HasKey(banReport => banReport.Id);
            builder.Property(banReport => banReport.Id).ValueGeneratedOnAdd();
            builder.Property(banReport => banReport.Reason).IsRequired();
            builder.Property(banReport => banReport.PerformedAction).IsRequired();
            builder.Property(banReport => banReport.PerformedAction)
                .HasConversion(
                    value => value.ToString(),
                    value => (UserBanAction)Enum.Parse(typeof(UserBanAction), value)
                );
            builder.HasOne(banReport => banReport.AffectedUser)
                .WithMany(user => user.AppliedBanActions)
                .HasForeignKey(banReport => banReport.AffectedUserId);
            builder.HasOne(banReport => banReport.Administrator)
                .WithMany(user => user.PerformedBanActions)
                .HasForeignKey(banReport => banReport.AdministratorId);
        }
    }
}
