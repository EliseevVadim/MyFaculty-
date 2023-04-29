using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFaculty.Domain.Entities
{
    public class Pair
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PairName { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int AuditoriumId { get; set; }
        public Auditorium Auditorium { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; }
        public int DayOfWeekId { get; set; }
        public WorkDayOfWeek DayOfWeek { get; set; }
        public int PairRepeatingId { get; set; }
        public PairRepeating PairRepeating { get; set; }
        public int PairInfoId { get; set; }
        public PairInfo PairInfo { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
