﻿using Microsoft.AspNetCore.Identity;

namespace MyFaculty.Identity.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
