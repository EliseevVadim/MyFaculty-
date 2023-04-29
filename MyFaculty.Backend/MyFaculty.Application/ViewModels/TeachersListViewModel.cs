using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class TeachersListViewModel
    {
        public IList<TeacherLookupDto> Teachers { get; set; }
    }
}
