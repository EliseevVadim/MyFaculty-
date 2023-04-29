using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Cities.Queries.GetCitiesForRegion
{
    public class GetCitiesForRegionQuery : IRequest<CitiesListViewModel>
    {
        public int RegionId { get; set; }
    }
}
