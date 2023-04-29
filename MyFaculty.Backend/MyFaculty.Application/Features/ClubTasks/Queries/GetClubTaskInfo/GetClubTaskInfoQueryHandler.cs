using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ClubTasks.Queries.GetClubTaskInfo
{
    public class GetClubTaskInfoQueryHandler : IRequestHandler<GetClubTaskInfoQuery, ClubTaskViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetClubTaskInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClubTaskViewModel> Handle(GetClubTaskInfoQuery request, CancellationToken cancellationToken)
        {
            ClubTask task = await _context.ClubTasks
                .Include(task => task.OwningStudyClub)
                    .ThenInclude(club => club.Moderators)
                .FirstOrDefaultAsync(task => task.Id == request.Id, cancellationToken);
            if (task == null)
                throw new EntityNotFoundException(nameof(ClubTask), request.Id);
            return _mapper.Map<ClubTaskViewModel>(task);
        }
    }
}
