using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.SecondaryObjects.Commands.UpdateSecondaryObject
{
    public class UpdateSecondaryObjectCommandHandler : IRequestHandler<UpdateSecondaryObjectCommand, SecondaryObject>
    {
        private IMFDbContext _context;

        public UpdateSecondaryObjectCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<SecondaryObject> Handle(UpdateSecondaryObjectCommand request, CancellationToken cancellationToken)
        {
            SecondaryObject secondaryObject = await _context.SecondaryObjects.FirstOrDefaultAsync(secondaryObject => secondaryObject.Id == request.Id, cancellationToken);
            if (secondaryObject == null)
                throw new EntityNotFoundException(nameof(SecondaryObject), request.Id);
            secondaryObject.FloorId = request.FloorId;
            secondaryObject.SecondaryObjectTypeId = request.SecondaryObjectTypeId;
            secondaryObject.PositionInfo = request.PositionInfo;
            secondaryObject.ObjectName = request.ObjectName;
            secondaryObject.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return secondaryObject;
        }
    }
}
