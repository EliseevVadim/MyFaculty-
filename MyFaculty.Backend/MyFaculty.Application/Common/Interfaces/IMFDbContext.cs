using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Common.Interfaces
{
    public interface IMFDbContext
    {
        DatabaseFacade Database { get; }
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<MyFaculty.Domain.Entities.WorkDayOfWeek> DaysOfWeek { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Pair> Pairs { get; set; }
        public DbSet<PairInfo> PairInfos { get; set; }
        public DbSet<PairRepeating> PairRepeatings { get; set; }
        public DbSet<ScienceRank> ScienceRanks { get; set; }
        public DbSet<SecondaryObject> SecondaryObjects { get; set; }
        public DbSet<SecondaryObjectType> SecondaryObjectTypes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherDiscipline> TeacherDisciplines { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<StudyClub> StudyClubs { get; set; }
        public DbSet<InformationPublic> InformationPublics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ClubTask> ClubTasks { get; set; }
        public DbSet<InfoPost> InfoPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ExpertSystemState> ExpertSystemStates { get; set; }
        public DbSet<ExpertSystemStateTransition> ExpertSystemStateTransitions { get; set; }
        public DbSet<ExpertSystemAnswer> ExpertSystemAnswers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        public async Task<int> ExecuteSqlRawAsync(string sql)
        {
            return await Database.ExecuteSqlRawAsync(sql);
        }
    }
}
