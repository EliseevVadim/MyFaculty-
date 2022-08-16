using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Groups.Commands.CreateGroup
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Group>
    {
        private IMFDbContext _context;

        public CreateGroupCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Group> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            Group group = new Group()
            {
                GroupName = request.GroupName,
                CourseId = request.CourseId,
            };
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync(cancellationToken);
            return group;
        }
    }
}
