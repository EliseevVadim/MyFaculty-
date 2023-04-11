using AutoMapper;
using AutoMapper.QueryableExtensions;
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

namespace MyFaculty.Application.Features.TaskSubmissions.Queries.GetSubmissionsForTask
{
    public class GetSubmissionsForTaskQueryHandler : IRequestHandler<GetSubmissionsForTaskQuery, TaskSubmissionsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetSubmissionsForTaskQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskSubmissionsListViewModel> Handle(GetSubmissionsForTaskQuery request, CancellationToken cancellationToken)
        {
            ClubTask owningTask = await _context.ClubTasks
                .Include(task => task.OwningStudyClub)
                    .ThenInclude(club => club.Moderators)
                .FirstOrDefaultAsync(task => task.Id == request.ClubTaskId, cancellationToken);
            if (owningTask == null)
                throw new EntityNotFoundException(nameof(ClubTask), request.ClubTaskId);
            if (!owningTask.OwningStudyClub.Moderators.Select(user => user.Id).Contains(request.IssuerId))
                throw new UnauthorizedActionException("Вы не можете просмотривать решения этого задания, поскольку не являетесь модератором сообщества.");
            var submissions = await _context.TaskSubmissions
                .Where(submission => submission.ClubTaskId == request.ClubTaskId)
                .OrderBy(submission => submission.Created)
                .ProjectTo<TaskSubmissionViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new TaskSubmissionsListViewModel()
            {
                TaskSubmissions = submissions
            };
        }
    }
}
