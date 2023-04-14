using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Users.Commands.TransferUsersToAnotherGroup
{
    public class TransferUsersToAnotherGroupCommandHandler : IRequestHandler<TransferUsersToAnotherGroupCommand>
    {
        private IMFDbContext _context;

        public TransferUsersToAnotherGroupCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(TransferUsersToAnotherGroupCommand request, CancellationToken cancellationToken)
        {
            Group sourceGroup = await _context.Groups.FindAsync(new object[] { request.SourceGroupId }, cancellationToken);
            if (sourceGroup == null)
                throw new EntityNotFoundException(nameof(Group), request.SourceGroupId);
            Group destinationGroup = await _context.Groups
                .Include(group => group.Course)
                .FirstOrDefaultAsync(group => group.Id == request.DestinationGroupId, cancellationToken);
            if (destinationGroup == null)
                throw new EntityNotFoundException(nameof(Group), request.DestinationGroupId);
            int destinationCourseId = destinationGroup.CourseId;
            int? destinationFacultyId = destinationGroup.Course.FacultyId;
            await _context.Users
                .Where(user => user.GroupId == request.SourceGroupId)
                .ForEachAsync(user =>
                {
                    user.GroupId = request.DestinationGroupId;
                    user.CourseId = destinationCourseId;
                    user.FacultyId = destinationFacultyId;
                });
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
