using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.SecondaryObjects.Commands.DeleteSecondaryObject
{
    public class DeleteSecondaryObjectCommandHandler : IRequestHandler<DeleteSecondaryObjectCommand>
    {
        private IMFDbContext _context;

        public DeleteSecondaryObjectCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSecondaryObjectCommand request, CancellationToken cancellationToken)
        {
            SecondaryObject secondaryObject = await _context.SecondaryObjects.FindAsync(new object[] { request.Id }, cancellationToken);
            if (secondaryObject == null)
                throw new EntityNotFoundException(nameof(SecondaryObject), request.Id);
            _context.SecondaryObjects.Remove(secondaryObject);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
