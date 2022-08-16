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

namespace MyFaculty.Application.Features.Disciplines.Commands.DeleteDiscipline
{
    public class DeleteDisciplineCommandHandler : IRequestHandler<DeleteDisciplineCommand>
    {
        private IMFDbContext _context;

        public DeleteDisciplineCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteDisciplineCommand request, CancellationToken cancellationToken)
        {
            Discipline discipline = await _context.Disciplines.FindAsync(new object[] { request.Id }, cancellationToken);
            if (discipline == null)
                throw new EntityNotFoundException(nameof(Discipline), request.Id);
            _context.Disciplines.Remove(discipline);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
