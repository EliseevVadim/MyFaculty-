using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence
{
    public class MFDbContext : DbContext
    {
        public MFDbContext(DbContextOptions<MFDbContext> options):
            base(options)
        { }

        public DbSet<Floor> Floors { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
