using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
