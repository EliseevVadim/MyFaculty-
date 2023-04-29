using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.PairInfos.Commands.DeletePairInfo
{
    public class DeletePairInfoCommandHandler : IRequestHandler<DeletePairInfoCommand>
    {
        private readonly IMFDbContext _context;

        public DeletePairInfoCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePairInfoCommand request, CancellationToken cancellationToken)
        {
            PairInfo pairInfo = await _context.PairInfos.FindAsync(new object[] { request.Id }, cancellationToken);
            if (pairInfo == null)
                throw new EntityNotFoundException(nameof(PairInfo), request.Id);
            _context.PairInfos.Remove(pairInfo);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
