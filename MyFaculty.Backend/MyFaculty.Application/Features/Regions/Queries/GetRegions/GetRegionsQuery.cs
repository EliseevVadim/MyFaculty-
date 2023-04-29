using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Regions.Queries.GetRegions
{
    public class GetRegionsQuery : IRequest<RegionsListViewModel>
    {

    }
}
