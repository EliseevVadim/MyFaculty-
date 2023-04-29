using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class PairRepeatingsListViewModel
    {
        public IList<PairRepeatingLookupDto> PairRepeatings { get; set; }
    }
}
