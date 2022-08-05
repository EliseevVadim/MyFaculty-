using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class Pair
    {
        public Guid Id { get; set; }
        public string PairName { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public Guid AuditriumId { get; set; }
        public Auditorium Auditorium { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public Guid DisciplineId { get; set; }
        public Discipline Discipline { get; set; }
        public Guid DayOfWeekId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public Guid PairRepeatingId { get; set; }
        public PairRepeating PairRepeating { get; set; }
        public Guid PairInfoId { get; set; }
        public PairInfo PairInfo { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
