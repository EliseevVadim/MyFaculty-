using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;

namespace MyFaculty.Application.Features.Floors.Commands.UpdateFloor
{
    public class UpdateFloorCommandHandler : IRequestHandler<UpdateFloorCommand, Floor>
    {
        private IMFDbContext _context;

        public UpdateFloorCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Floor> Handle(UpdateFloorCommand request, CancellationToken cancellationToken)
        {
            Floor floor = await _context.Floors.FirstOrDefaultAsync(floor => floor.Id == request.Id, cancellationToken);
            if (floor == null)
                throw new EntityNotFoundException(nameof(Floor), request.Id);
            floor.Name = request.Name;
            floor.Bounds = request.Bounds;
            floor.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return floor;
        }
    }
}
