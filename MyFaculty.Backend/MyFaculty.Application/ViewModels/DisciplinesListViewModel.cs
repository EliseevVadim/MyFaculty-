using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class DisciplinesListViewModel
    {
        public IList<DisciplineLookupDto> Disciplines { get; set; }
    }
}
