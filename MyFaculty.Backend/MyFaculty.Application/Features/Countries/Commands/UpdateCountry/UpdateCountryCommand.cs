using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommand : IRequest<CountryViewModel>
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
    }
}
