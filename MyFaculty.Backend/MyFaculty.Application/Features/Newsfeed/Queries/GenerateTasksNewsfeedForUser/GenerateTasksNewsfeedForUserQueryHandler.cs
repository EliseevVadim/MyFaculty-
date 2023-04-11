using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Newsfeed.Queries.GenerateTasksNewsfeedForUser
{
    public class GenerateTasksNewsfeedForUserQueryHandler : IRequestHandler<GenerateTasksNewsfeedForUserQuery, ClubTasksListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        private const int TASKS_COUNT_LIMIT = 100;

        public GenerateTasksNewsfeedForUserQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClubTasksListViewModel> Handle(GenerateTasksNewsfeedForUserQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await _context.Users
                .Include(user => user.StudyClubs)
                    .ThenInclude(club => club.ClubTasks)
                        .ThenInclude(task => task.OwningStudyClub)
                            .ThenInclude(club => club.Moderators)
                .Include(user => user.StudyClubs)
                    .ThenInclude(club => club.Moderators)
                .Include(user => user.StudyClubs)
                    .ThenInclude(club => club.ClubTasks)
                        .ThenInclude(task => task.Comments)
                .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
            if (user == null)
                throw new EntityNotFoundException(nameof(AppUser), request.UserId);
            var clubTasks = user.StudyClubs
                .SelectMany(club => club.ClubTasks)
                .OrderByDescending(task => task.Created)
                .Take(TASKS_COUNT_LIMIT)
                .Select(club => _mapper.Map<ClubTaskViewModel>(club))              
                .ToList();
            return new ClubTasksListViewModel()
            {
                ClubTasks = clubTasks
            };
        }
    }
}
