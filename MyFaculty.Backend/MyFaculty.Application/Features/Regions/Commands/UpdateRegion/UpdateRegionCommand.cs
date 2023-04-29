using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Regions.Commands.UpdateRegion
{
    public class UpdateRegionCommand : IRequest<RegionViewModel>
    {
        public int Id { get; set; }
        public string RegionName { get; set; }
        public int CountryId { get; set; }
    }
}
