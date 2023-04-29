using Microsoft.EntityFrameworkCore;
using MyFaculty.Persistence;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Services
{
    public class RolesService
    {
        public async Task AddUserToRoleAsync(int userId, string roleName)
        {
            using (MFDbContext db = new MFDbContext())
            {
                string query = $"INSERT INTO userroles (UserId, RoleId) VALUES ({userId}, (SELECT id FROM roles WHERE NormalizedName = '{roleName}'))";
                await db.Database.ExecuteSqlRawAsync(query);
            }
        }
    }
}
