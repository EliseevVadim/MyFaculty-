using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class StudyClubConfiguration : IEntityTypeConfiguration<StudyClub>
    {
        public void Configure(EntityTypeBuilder<StudyClub> builder)
        {
            builder.HasKey(club => club.Id);
            builder.Property(club => club.Id).ValueGeneratedOnAdd();
            builder.Property(club => club.Description).IsRequired(false);
            builder.Property(club => club.ImagePath).IsRequired(false);
            builder.Property(club => club.ClubName).IsRequired();
            builder.HasIndex(club => club.ClubName).IsUnique();
            builder.HasOne(club => club.Owner)
                .WithMany(user => user.OwnedStudyClubs)
                .HasForeignKey(club => club.OwnerId);
            builder.HasMany(club => club.Members)
                .WithMany(user => user.StudyClubs)
                .UsingEntity(j => j.ToTable("UsersAtStudyClubs"));
            builder.HasMany(club => club.Moderators)
                .WithMany(user => user.StudyClubsAtModeration)
                .UsingEntity(j => j.ToTable("UsersModeratesClubs"));
            builder.HasMany(club => club.ClubTasks)
                .WithOne(task => task.OwningStudyClub)
                .HasForeignKey(task => task.StudyClubId);
            builder.HasMany(club => club.InfoPosts)
                .WithOne(post => post.OwningStudyClub)
                .HasForeignKey(post => post.StudyClubId);
        }
    }
}
