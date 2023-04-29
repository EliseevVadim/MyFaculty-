using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Queries.GetSecondaryObjectTypes
{
    public class GetSecondaryObjectTypesQuery : IRequest<SecondaryObjectTypesListViewModel>
    {

    }
}
