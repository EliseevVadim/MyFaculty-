using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.SecondaryObjects.Commands.CreateSecondaryObject
{
    public class CreateSecondaryObjectCommandHandler : IRequestHandler<CreateSecondaryObjectCommand, SecondaryObject>
    {
        private IMFDbContext _context;

        public CreateSecondaryObjectCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<SecondaryObject> Handle(CreateSecondaryObjectCommand request, CancellationToken cancellationToken)
        {
            SecondaryObject secondaryObject = new SecondaryObject()
            {
                ObjectName = request.ObjectName,
                PositionInfo = request.PositionInfo,
                FloorId = request.FloorId,
                SecondaryObjectTypeId = request.SecondaryObjectTypeId,
                Created = DateTime.Now
            };
            await _context.SecondaryObjects.AddAsync(secondaryObject, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return secondaryObject;
        }
    }
}
