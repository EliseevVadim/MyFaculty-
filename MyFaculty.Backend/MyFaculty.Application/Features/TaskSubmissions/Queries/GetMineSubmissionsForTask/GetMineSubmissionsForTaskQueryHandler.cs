using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissions.Queries.GetMineSubmissionsForTask
{
    public class GetMineSubmissionsForTaskQueryHandler : IRequestHandler<GetMineSubmissionsForTaskQuery, TaskSubmissionsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetMineSubmissionsForTaskQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskSubmissionsListViewModel> Handle(GetMineSubmissionsForTaskQuery request, CancellationToken cancellationToken)
        {
            ClubTask owningTask = await _context.ClubTasks.FirstOrDefaultAsync(task => task.Id == request.ClubTaskId, cancellationToken);
            if (owningTask == null)
                throw new EntityNotFoundException(nameof(ClubTask), request.ClubTaskId);
            var submissions = await _context.TaskSubmissions
                .Where(submission => submission.ClubTaskId == request.ClubTaskId && submission.AuthorId == request.IssuerId)
                .OrderByDescending(submission => submission.Created)
                .ProjectTo<TaskSubmissionViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new TaskSubmissionsListViewModel()
            {
                TaskSubmissions = submissions
            };
        }
    }
}
