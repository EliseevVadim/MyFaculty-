using System;
using System.Collections.Generic;

namespace MyFaculty.Domain.Entities
{
    public class ClubTask : Post
    {
        public int StudyClubId { get; set; }
        public StudyClub OwningStudyClub { get; set; }
        public DateTime DeadLine { get; set; }
        public int Cost { get; set; }
        public List<TaskSubmission> Submissions { get; set; }
    }
}
