using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MyFaculty.Application.Features.Cities.Commands.DeleteCity
{
    public class DeleteCityCommand : IRequest
    {
        public int Id { get; set; }
    }
}
