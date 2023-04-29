using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class InfoPostConfiguration : IEntityTypeConfiguration<InfoPost>
    {
        public void Configure(EntityTypeBuilder<InfoPost> builder)
        {
            builder.ToTable("infoPosts");
            builder.Property(post => post.InfoPublicId).IsRequired(false);
            builder.Property(post => post.StudyClubId).IsRequired(false);
            builder.HasOne(post => post.OwningStudyClub)
                .WithMany(club => club.InfoPosts)
                .HasForeignKey(post => post.StudyClubId);
            builder.HasOne(post => post.OwningInformationPublic)
                .WithMany(infoPublic => infoPublic.InfoPosts)
                .HasForeignKey(post => post.InfoPublicId);
            builder.HasMany(post => post.LikedUsers)
                .WithMany(user => user.LikedPosts)
                .UsingEntity(j => j.ToTable("userslikedposts"));
        }
    }
}
