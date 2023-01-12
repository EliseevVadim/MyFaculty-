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

namespace MyFaculty.Application.Features.StudyClubs.Commands.DeleteStudyClub
{
    public class DeleteStudyClubCommandHandler : IRequestHandler<DeleteStudyClubCommand>
    {
        private IMFDbContext _context;

        public DeleteStudyClubCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteStudyClubCommand request, CancellationToken cancellationToken)
        {
            StudyClub club = await _context.StudyClubs.FindAsync(new object[] { request.Id }, cancellationToken);
            if (club == null)
                throw new EntityNotFoundException(nameof(StudyClub), request.Id);
            _context.StudyClubs.Remove(club);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
