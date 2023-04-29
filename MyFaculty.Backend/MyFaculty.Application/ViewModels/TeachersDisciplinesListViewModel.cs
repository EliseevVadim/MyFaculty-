using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class TeachersDisciplinesListViewModel
    {
        public IList<TeacherDisciplineLookupDto> TeacherDisciplines { get; set; }
    }
}
