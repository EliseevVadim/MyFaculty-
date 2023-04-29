using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommand : IRequest<CountryViewModel>
    {
        public string CountryName { get; set; }
    }
}
