using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence
{
    public class MFDbContext : DbContext, IMFDbContext
    {
        public MFDbContext():
            base()
        { }

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
        public DbSet<Faculty> Faculties { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Region> Regions { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<AppUser> Users { get; set; } = null!;
        public DbSet<StudyClub> StudyClubs { get; set; } = null!;
        public DbSet<InformationPublic> InformationPublics { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<ClubTask> ClubTasks { get; set; } = null!;
        public DbSet<InfoPost> InfoPosts { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<TaskSubmission> TaskSubmissions { get; set; } = null!;
        public DbSet<TaskSubmissionReview> TaskSubmissionReviews { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<UserBanReport> UsersBansReports { get; set; } = null!;
        public DbSet<ExpertSystemState> ExpertSystemStates { get; set; } = null!;
        public DbSet<ExpertSystemStateTransition> ExpertSystemStateTransitions { get; set; } = null!;
        public DbSet<ExpertSystemAnswer> ExpertSystemAnswers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory().Replace("Persistence", "WebApi"))
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseMySql(configuration["ConnectionString"], new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
