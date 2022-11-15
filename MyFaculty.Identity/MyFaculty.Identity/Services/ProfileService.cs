using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using MyFaculty.Identity.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFaculty.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private UserManager<AppUser> _userManager;

        public ProfileService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            AppUser user = await _userManager.GetUserAsync(context.Subject);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };
            List<Claim> roles = context.Subject.FindAll(JwtClaimTypes.Role).ToList();
            context.IssuedClaims.AddRange(claims);
            context.IssuedClaims.AddRange(roles);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            AppUser user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = user != null;
        }
    }
}
