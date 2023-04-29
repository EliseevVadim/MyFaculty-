using System;
using System.Collections.Generic;

namespace MyFaculty.Domain.Entities
{
    public class Faculty
    {
        public int Id { get; set; }
        public string FacultyName { get; set; }
        public string OfficialWebsite { get; set; }
        public List<Floor> Floors { get; set; }
        public List<Course> Courses { get; set; }
        public List<AppUser> Students { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
