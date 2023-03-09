using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class ClubTask : Post
    {
        public int StudyClubId { get; set; }
        public StudyClub OwningStudyClub { get; set; }
        public DateTime DeadLine { get; set; }
        public int Cost { get; set; }
    }
}
