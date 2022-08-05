using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class TeacherDiscipline
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public Guid DisciplineId { get; set; }
        public Discipline Discipline { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
