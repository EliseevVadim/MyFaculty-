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

namespace MyFaculty.Application.Features.Groups.Commands.DeleteGroup
{
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand>
    {
        private IMFDbContext _context;

        public DeleteGroupCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            Group group = await _context.Groups.FindAsync(new object[] { request.Id }, cancellationToken);
            if (group == null)
                throw new EntityNotFoundException(nameof(Group), request.Id);
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
