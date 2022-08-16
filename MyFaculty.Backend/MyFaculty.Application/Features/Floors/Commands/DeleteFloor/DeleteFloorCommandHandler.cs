﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Floors.Commands.DeleteFloor
{
    public class DeleteFloorCommandHandler : IRequestHandler<DeleteFloorCommand>
    {
        private IMFDbContext _context;

        public DeleteFloorCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFloorCommand request, CancellationToken cancellationToken)
        {
            Floor floor = await _context.Floors.FindAsync(new object[] { request.Id }, cancellationToken);
            if (floor == null)
                throw new EntityNotFoundException(nameof(Floor), request.Id);
            _context.Floors.Remove(floor);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
