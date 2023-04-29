using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class PostOwnerViewModel
    {
        public string OwnerName { get; set; }
        public string OwnerAvatar { get; set; }
        public string OwnerLink { get; set; }
        public List<int> ModeratorsIds { get; set; }
        public bool IsBanned { get; set; }
        public int OwningUserId { get; set; }
    }
}
