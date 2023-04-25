using Microsoft.AspNetCore.Identity;
using System;

namespace MyFaculty.Identity.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarPath { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }
        public string Website { get; set; }
        public string VKLink { get; set; }
        public string TelegramLink { get; set; }
        public bool IsBanned { get; set; }
    }
}
