using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class AuditoriumsListViewModel
    {
        public IList<AuditoriumLookupDto> Auditoriums { get; set; }
    }
}
