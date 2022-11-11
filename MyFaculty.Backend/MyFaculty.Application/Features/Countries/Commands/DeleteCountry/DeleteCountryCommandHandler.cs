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

namespace MyFaculty.Application.Features.Countries.Commands.DeleteCountry
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand>
    {
        private IMFDbContext _context;

        public DeleteCountryCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            Country country = await _context.Countries.FindAsync(new object[] { request.Id }, cancellationToken);
            if (country == null)
                throw new EntityNotFoundException(nameof(Country), request.Id);
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
