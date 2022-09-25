using MediatR;
using MyFaculty.Application.ViewModels.ExpertSystemViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ExpertSystem.States.Queries.GetStates
{
    public class GetStatesQuery : IRequest<StatesListViewModel>
    {

    }
}
