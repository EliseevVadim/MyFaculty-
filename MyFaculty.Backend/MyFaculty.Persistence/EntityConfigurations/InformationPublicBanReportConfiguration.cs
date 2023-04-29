using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;
using MyFaculty.Domain.Enums;
using System;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class InformationPublicBanReportConfiguration : IEntityTypeConfiguration<InformationPublicBanReport>
    {
        public void Configure(EntityTypeBuilder<InformationPublicBanReport> builder)
        {
            builder.HasKey(banReport => banReport.Id);
            builder.Property(banReport => banReport.Id).ValueGeneratedOnAdd();
            builder.Property(banReport => banReport.Reason).IsRequired();
            builder.Property(banReport => banReport.PerformedAction).IsRequired();
            builder.Property(banReport => banReport.PerformedAction)
                .HasConversion(
                    value => value.ToString(),
                    value => (BanAction)Enum.Parse(typeof(BanAction), value)
                );
            builder.HasOne(banReport => banReport.AffectedPublic)
                .WithMany(infoPublic => infoPublic.AppliedBanReports)
                .HasForeignKey(banReport => banReport.PublicId);
            builder.HasOne(banReport => banReport.Administrator)
                .WithMany(user => user.InformationsPublicBanReports)
                .HasForeignKey(banReport => banReport.AdministratorId);
        }
    }
}
