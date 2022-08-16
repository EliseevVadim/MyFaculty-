using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence
{
    public class MFDbContext : DbContext, IMFDbContext
    {
        public MFDbContext(DbContextOptions<MFDbContext> options):
            base(options)
        { }

        public DbSet<Auditorium> Auditoriums { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<MyFaculty.Domain.Entities.WorkDayOfWeek> DaysOfWeek { get; set; } = null!;
        public DbSet<Discipline> Disciplines { get; set; } = null!;
        public DbSet<Floor> Floors { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Pair> Pairs { get; set; } = null!;
        public DbSet<PairInfo> PairInfos { get; set; } = null!;
        public DbSet<PairRepeating> PairRepeatings { get; set; } = null!;
        public DbSet<ScienceRank> ScienceRanks { get; set; } = null!;
        public DbSet<SecondaryObject> SecondaryObjects { get; set; } = null!;
        public DbSet<SecondaryObjectType> SecondaryObjectTypes { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<TeacherDiscipline> TeacherDisciplines { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
