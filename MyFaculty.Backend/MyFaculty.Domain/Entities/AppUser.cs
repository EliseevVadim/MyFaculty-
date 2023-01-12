using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public City City { get; set; }
        public Faculty Faculty { get; set; }
        public Course Course { get; set; }
        public Group Group { get; set; }
        public List<StudyClub> StudyClubs { get; set; } = new List<StudyClub>();
        public List<StudyClub> OwnedStudyClubs { get; set; } = new List<StudyClub>();
        public List<StudyClub> StudyClubsAtModeration { get; set; } = new List<StudyClub>();
    }
}
