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

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.UpdateSecondaryObjectType
{
    public class UpdateSecondaryObjectTypeCommandHandler : IRequestHandler<UpdateSecondaryObjectTypeCommand, SecondaryObjectType>
    {
        private IMFDbContext _context;

        public UpdateSecondaryObjectTypeCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<SecondaryObjectType> Handle(UpdateSecondaryObjectTypeCommand request, CancellationToken cancellationToken)
        {
            SecondaryObjectType type = await _context.SecondaryObjectTypes.FirstOrDefaultAsync(type => type.Id == request.Id, cancellationToken);
            if (type == null)
                throw new EntityNotFoundException(nameof(SecondaryObjectType), request.Id);
            type.ObjectTypeName = request.ObjectTypeName;
            type.TypePath = request.TypePath;
            type.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return type;
        }
    }
}
