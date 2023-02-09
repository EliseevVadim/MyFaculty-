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
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("posts");
            builder.HasKey(post => post.Id);
            builder.Property(post => post.Id).ValueGeneratedOnAdd();
            builder.Property(post => post.TextContent).IsRequired(false);
            builder.Property(post => post.Attachments).IsRequired(false);
            builder.Property(post => post.PostAttachmentsUid).IsRequired();
            builder.HasOne(post => post.Author)
                .WithMany(user => user.Posts)
                .HasForeignKey(post => post.AuthorId);
        }
    }
}
