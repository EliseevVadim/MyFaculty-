using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Regions.Queries.GetRegionInfo
{
    public class GetRegionInfoQuery : IRequest<RegionViewModel>
    {
        public int Id { get; set; }
    }
}
