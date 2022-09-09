using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MyFaculty.Identity.Configurations
{
    public static class RolesManager
    {
        private static string[] _roles = new[]
        {
            "User",
            "Admin"
        };

        public static IServiceCollection AddRolesManager(this IServiceCollection services)
        {
            SeedRoles(services);
            return services;
        } 

        private static async void SeedRoles(IServiceCollection services)
        {
            var rolesManager = services
                .BuildServiceProvider()
                .GetRequiredService<RoleManager<IdentityRole<int>>>();
            bool roleExists = false;
            foreach (string role in _roles)
            {
                roleExists = await rolesManager.RoleExistsAsync(role);
                if (!roleExists)
                    await rolesManager.CreateAsync(new IdentityRole<int>(role));
            }
        }
    }
}
