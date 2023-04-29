using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Countries.Queries.GetCountryInfo
{
    public class GetCountryInfoQuery : IRequest<CountryViewModel>
    {
        public int Id { get; set; }
    }
}
