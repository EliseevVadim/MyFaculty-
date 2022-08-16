using MyFaculty.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels
{
    public class CoursesListViewModel
    {
        public IList<CourseLookupDto> Courses { get; set; }
    }
}
