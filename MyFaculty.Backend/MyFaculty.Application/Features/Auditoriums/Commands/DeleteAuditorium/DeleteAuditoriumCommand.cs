using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MyFaculty.Application.Features.Auditoriums.Commands.DeleteAuditorium
{
    public class DeleteAuditoriumCommand : IRequest
    {
        public int Id { get; set; }
    }
}
