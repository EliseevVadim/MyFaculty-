using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ClubTasks.Queries.GetClubTasksForStudyClub
{
    public class GetClubTasksForStudyClubQueryHandler : IRequestHandler<GetClubTasksForStudyClubQuery, ClubTasksListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetClubTasksForStudyClubQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClubTasksListViewModel> Handle(GetClubTasksForStudyClubQuery request, CancellationToken cancellationToken)
        {
            var clubTasks = await _context.ClubTasks
                .Include(task => task.OwningStudyClub)
                .Where(task => task.StudyClubId == request.StudyClubId)
                .OrderByDescending(task => task.Created)
                .ProjectTo<ClubTaskViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new ClubTasksListViewModel()
            {
                ClubTasks = clubTasks
            };
        }
    }
}
