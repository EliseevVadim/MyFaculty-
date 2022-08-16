using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Groups.Commands.UpdateGroup
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Group>
    {
        private IMFDbContext _context;

        public UpdateGroupCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Group> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            Group group = await _context.Groups.FirstOrDefaultAsync(group => group.Id == request.Id, cancellationToken);
            if (group == null)
                throw new EntityNotFoundException(nameof(Group), request.Id);
            group.GroupName = request.GroupName;
            group.CourseId = request.CourseId;
            await _context.SaveChangesAsync(cancellationToken);
            return group;
        }
    }
}
