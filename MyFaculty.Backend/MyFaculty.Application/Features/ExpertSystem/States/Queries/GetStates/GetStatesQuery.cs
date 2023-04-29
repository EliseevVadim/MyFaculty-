using MediatR;
using MyFaculty.Application.ViewModels.ExpertSystemViewModels;

namespace MyFaculty.Application.Features.ExpertSystem.States.Queries.GetStates
{
    public class GetStatesQuery : IRequest<StatesListViewModel>
    {

    }
}
