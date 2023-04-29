using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Regions.Commands.CreateRegion
{
    public class CreateRegionCommand : IRequest<RegionViewModel>
    {
        public string RegionName { get; set; }
        public int CountryId { get; set; }
    }
}
