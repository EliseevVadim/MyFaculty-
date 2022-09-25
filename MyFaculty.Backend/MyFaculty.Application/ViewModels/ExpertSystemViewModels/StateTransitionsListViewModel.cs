using MyFaculty.Application.Dto.ExpertSystemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels.ExpertSystemViewModels
{
    public class StateTransitionsListViewModel
    {
        public IList<StateTransitionLookupDto> StateTransitions { get; set; }
    }
}
