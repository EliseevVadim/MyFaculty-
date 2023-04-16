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
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(notification => notification.Id);
            builder.Property(notification => notification.Id).ValueGeneratedOnAdd();
            builder.Property(notification => notification.TextContent).IsRequired();
            builder.Property(notification => notification.UserId).IsRequired();
            builder.Property(notification => notification.ReturnUrl).IsRequired(false);
            builder.Property(notification => notification.MetaInfo).IsRequired(false);
            builder.HasOne(notification => notification.NotifiedUser)
                .WithMany(user => user.Notifications)
                .HasForeignKey(notification => notification.UserId);
        }
    }
}
