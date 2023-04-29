using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class ScienceRanksListViewModel
    {
        public IList<ScienceRankLookupDto> ScienceRanks { get; set; }
    }
}
