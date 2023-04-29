using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Countries.Queries.GetCountries
{
    public class GetCountriesQuery : IRequest<CountriesListViewModel>
    {

    }
}
