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

namespace MyFaculty.Application.Features.Auditoriums.Commands.DeleteAuditorium
{
    public class DeleteAuditoriumCommandHandler : IRequestHandler<DeleteAuditoriumCommand>
    {
        private IMFDbContext _context;

        public DeleteAuditoriumCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteAuditoriumCommand request, CancellationToken cancellationToken)
        {
            Auditorium auditorium = await _context.Auditoriums.FindAsync(new object[] { request.Id }, cancellationToken);
            if (auditorium == null)
                throw new EntityNotFoundException(nameof(Auditorium), request.Id);
            _context.Auditoriums.Remove(auditorium);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
