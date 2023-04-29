using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Disciplines.Commands.DeleteDiscipline
{
    public class DeleteDisciplineCommandHandler : IRequestHandler<DeleteDisciplineCommand>
    {
        private readonly IMFDbContext _context;

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
