using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Faculties.Commands.DeleteFaculty
{
    public class DeleteFacultyCommandHandler : IRequestHandler<DeleteFacultyCommand>
    {
        private IMFDbContext _context;

        public DeleteFacultyCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFacultyCommand request, CancellationToken cancellationToken)
        {
            Faculty faculty = await _context.Faculties.FindAsync(new object[] { request.Id }, cancellationToken);
            if (faculty == null)
                throw new EntityNotFoundException(nameof(Faculty), request.Id);
            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
