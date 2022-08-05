using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(MFDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
