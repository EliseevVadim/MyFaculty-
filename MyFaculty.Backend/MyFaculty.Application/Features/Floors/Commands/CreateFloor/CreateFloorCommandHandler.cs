using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Floors.Commands.CreateFloor
{
    public class CreateFloorCommandHandler : IRequestHandler<CreateFloorCommand, Floor>
    {
        private IMFDbContext _context;

        public CreateFloorCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Floor> Handle(CreateFloorCommand request, CancellationToken cancellationToken)
        {
            Floor floor = new Floor
            {
                Name = request.Name,
                Bounds = request.Bounds,
                Created = DateTime.Now
            };
            await _context.Floors.AddAsync(floor);
            await _context.SaveChangesAsync(cancellationToken);
            return floor;
        }
    }
}
