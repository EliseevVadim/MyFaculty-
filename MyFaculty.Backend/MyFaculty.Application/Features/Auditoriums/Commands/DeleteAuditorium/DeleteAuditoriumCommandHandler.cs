using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Auditoriums.Commands.DeleteAuditorium
{
    public class DeleteAuditoriumCommandHandler : IRequestHandler<DeleteAuditoriumCommand>
    {
        private readonly IMFDbContext _context;

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
