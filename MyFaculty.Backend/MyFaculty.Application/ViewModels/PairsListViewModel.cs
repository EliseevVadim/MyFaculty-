using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class PairsListViewModel
    {
        public IList<PairLookupDto> Pairs { get; set; }
    }
}
