using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Groups.Commands.UpdateGroup
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, GroupViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public UpdateGroupCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GroupViewModel> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            Group group = await _context.Groups.FirstOrDefaultAsync(group => group.Id == request.Id, cancellationToken);
            if (group == null)
                throw new EntityNotFoundException(nameof(Group), request.Id);
            group.GroupName = request.GroupName;
            group.CourseId = request.CourseId;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<GroupViewModel>(group);
        }
    }
}
