using Microsoft.EntityFrameworkCore;
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

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
