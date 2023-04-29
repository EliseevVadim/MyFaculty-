using MyFaculty.Application.Dto.ExpertSystemDto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels.ExpertSystemViewModels
{
    public class StateTransitionsListViewModel
    {
        public IList<StateTransitionLookupDto> StateTransitions { get; set; }
    }
}
