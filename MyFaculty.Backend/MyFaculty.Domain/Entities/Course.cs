using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public int CourseNumber { get; set; }
        public List<Group> Groups { get; set; } = new();
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
