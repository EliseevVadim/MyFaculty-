using MediatR;
using MyFaculty.Application.ViewModels.ExpertSystemViewModels;

namespace MyFaculty.Application.Features.ExpertSystem.StateTransitions.Queries.GetStateTransitions
{
    public class GetStateTransitionsQuery : IRequest<StateTransitionsListViewModel>
    {

    }
}
