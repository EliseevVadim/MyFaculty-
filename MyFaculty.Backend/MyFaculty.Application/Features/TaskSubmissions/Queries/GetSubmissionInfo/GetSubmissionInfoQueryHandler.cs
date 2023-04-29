using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissions.Queries.GetSubmissionInfo
{
    public class GetSubmissionInfoQueryHandler : IRequestHandler<GetSubmissionInfoQuery, TaskSubmissionViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetSubmissionInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskSubmissionViewModel> Handle(GetSubmissionInfoQuery request, CancellationToken cancellationToken)
        {
            TaskSubmission submission = await _context.TaskSubmissions
                .Include(submission => submission.ClubTask)
                    .ThenInclude(task => task.OwningStudyClub)
                        .ThenInclude(club => club.Moderators)
                .FirstOrDefaultAsync(submission => submission.Id == request.Id, cancellationToken);
            if (submission == null)
                throw new EntityNotFoundException(nameof(TaskSubmission), request.Id);
            if (!submission.ClubTask.OwningStudyClub.Moderators.Select(user => user.Id).Contains(request.IssuerId))
                throw new UnauthorizedActionException("Вы не можете просмотривать решения этого задания, поскольку не являетесь модератором сообщества.");
            return _mapper.Map<TaskSubmissionViewModel>(submission);
        }
    }
}
