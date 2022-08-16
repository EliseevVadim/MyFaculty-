using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Floors.Commands.UpdateFloor
{
    public class UpdateFloorCommand : IRequest<Floor>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bounds { get; set; }
    }
}
