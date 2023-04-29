using System;
using System.Collections.Generic;

namespace MyFaculty.Domain.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarPath { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }
        public int? FacultyId { get; set; }
        public int? CourseId { get; set; }
        public int? GroupId { get; set; }
        public bool IsTeacher { get; set; }
        public string Website { get; set; }
        public string VKLink { get; set; }
        public string TelegramLink { get; set; }
        public bool IsBanned { get; set; }
        public City City { get; set; }
        public Faculty Faculty { get; set; }
        public Course Course { get; set; }
        public Group Group { get; set; }
        public List<StudyClub> StudyClubs { get; set; } = new List<StudyClub>();
        public List<InformationPublic> InformationPublics { get; set; } = new List<InformationPublic>();
        public List<StudyClub> OwnedStudyClubs { get; set; } = new List<StudyClub>();
        public List<InformationPublic> OwnedInformationPublics { get; set; } = new List<InformationPublic>();
        public List<StudyClub> StudyClubsAtModeration { get; set; } = new List<StudyClub>();
        public List<InformationPublic> InformationPublicsAtModeration { get; set; } = new List<InformationPublic>();
        public List<InformationPublic> BlockedPublics { get; set; } = new List<InformationPublic>();
        public List<Post> Posts { get; set; }
        public List<InfoPost> LikedPosts { get; set; } = new List<InfoPost>();
        public List<Comment> Comments { get; set; }
        public List<TaskSubmission> TaskSubmissions { get; set; }
        public List<TaskSubmissionReview> SubmissionReviews { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<UserBanReport> AppliedBanActions { get; set; }
        public List<UserBanReport> PerformedBanActions { get; set; }
        public List<InformationPublicBanReport> InformationsPublicBanReports { get; set; }
    }
}
