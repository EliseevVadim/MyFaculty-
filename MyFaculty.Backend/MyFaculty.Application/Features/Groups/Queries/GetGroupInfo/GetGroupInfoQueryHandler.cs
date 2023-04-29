using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroupInfo
{
    public class GetGroupInfoQueryHandler : IRequestHandler<GetGroupInfoQuery, GroupViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetGroupInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GroupViewModel> Handle(GetGroupInfoQuery request, CancellationToken cancellationToken)
        {
            Group group = await _context.Groups
                .Include(group => group.Course)
                    .ThenInclude(course => course.Faculty)
                .FirstOrDefaultAsync(group => group.Id == request.Id, cancellationToken);
            if (group == null)
                throw new EntityNotFoundException(nameof(Group), request.Id);
            return _mapper.Map<GroupViewModel>(group);
        }
    }
}
