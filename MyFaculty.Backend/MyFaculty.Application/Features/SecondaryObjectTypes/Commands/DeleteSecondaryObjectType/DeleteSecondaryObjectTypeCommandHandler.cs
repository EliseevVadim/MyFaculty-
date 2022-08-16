using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.DeleteSecondaryObjectType
{
    public class DeleteSecondaryObjectTypeCommandHandler : IRequestHandler<DeleteSecondaryObjectTypeCommand>
    {
        private IMFDbContext _context;

        public DeleteSecondaryObjectTypeCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSecondaryObjectTypeCommand request, CancellationToken cancellationToken)
        {
            SecondaryObjectType type = await _context.SecondaryObjectTypes.FindAsync(new object[] { request.Id }, cancellationToken);
            if (type == null)
                throw new EntityNotFoundException(nameof(SecondaryObjectType), request.Id);
            _context.SecondaryObjectTypes.Remove(type);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
