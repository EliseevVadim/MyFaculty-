using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class CountriesListViewModel
    {
        public IList<CountryLookupDto> Countries { get; set; }
    }
}
