using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Cities.Commands.DeleteCity
{
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand>
    {
        private readonly IMFDbContext _context;

        public DeleteCityCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            City city = await _context.Cities.FindAsync(new object[] { request.Id }, cancellationToken);
            if (city == null)
                throw new EntityNotFoundException(nameof(City), request.Id);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
