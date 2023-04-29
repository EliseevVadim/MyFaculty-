using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class CoursesListViewModel
    {
        public IList<CourseLookupDto> Courses { get; set; }
    }
}
