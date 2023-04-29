using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Regions.Queries.GetRegionsForCountry
{
    public class GetRegionsForCountryQuery : IRequest<RegionsListViewModel>
    {
        public int CountryId { get; set; }
    }
}
