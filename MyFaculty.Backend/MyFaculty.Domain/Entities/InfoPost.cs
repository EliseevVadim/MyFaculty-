using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class InfoPost : Post
    {
        public int? StudyClubId { get; set; }
        public StudyClub OwningStudyClub { get; set; }
        public int? InfoPublicId { get; set; }
        public InformationPublic OwningInformationPublic { get; set; }
        public List<AppUser> LikedUsers { get; set; } = new List<AppUser>();
        public bool CommentsAllowed { get; set; }
    }
}
