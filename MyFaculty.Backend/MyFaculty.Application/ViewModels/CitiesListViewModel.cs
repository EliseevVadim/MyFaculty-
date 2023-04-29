using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class CitiesListViewModel
    {
        public IList<CityLookupDto> Cities { get; set; }
    }
}
