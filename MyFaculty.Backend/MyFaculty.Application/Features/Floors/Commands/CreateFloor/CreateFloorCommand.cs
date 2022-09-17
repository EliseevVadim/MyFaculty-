using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Floors.Commands.CreateFloor
{
    public class CreateFloorCommand : IRequest<FloorViewModel>
    {
        public string Name { get; set; }
        public string Bounds { get; set; }
        public int FacultyId { get; set; }
    }
}
