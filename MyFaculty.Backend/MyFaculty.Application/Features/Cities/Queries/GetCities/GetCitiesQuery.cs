using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Cities.Queries.GetCities
{
    public class GetCitiesQuery : IRequest<CitiesListViewModel>
    {

    }
}
