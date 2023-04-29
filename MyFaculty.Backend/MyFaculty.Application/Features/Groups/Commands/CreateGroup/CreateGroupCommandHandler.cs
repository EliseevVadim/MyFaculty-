using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Groups.Commands.CreateGroup
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, GroupViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public CreateGroupCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GroupViewModel> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            Group group = new Group()
            {
                GroupName = request.GroupName,
                CourseId = request.CourseId,
            };
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<GroupViewModel>(group);
        }
    }
}
