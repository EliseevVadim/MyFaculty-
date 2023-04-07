using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using MyFaculty.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissions.Commands.CreateTaskSubmission
{
    public class CreateTaskSubmissionCommandHandler : IRequestHandler<CreateTaskSubmissionCommand, TaskSubmissionViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreateTaskSubmissionCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskSubmissionViewModel> Handle(CreateTaskSubmissionCommand request, CancellationToken cancellationToken)
        {
            ClubTask task = await _context.ClubTasks.FirstOrDefaultAsync(task => task.Id == request.ClubTaskId, cancellationToken);
            if (task == null)
                throw new EntityNotFoundException(nameof(ClubTask), request.ClubTaskId);
            AppUser author = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.AuthorId, cancellationToken);
            if (author == null)
                throw new EntityNotFoundException(nameof(AppUser), request.AuthorId);
            TaskSubmission submission = new TaskSubmission()
            {
                Title = request.Title,
                TextContent = request.TextContent,
                Attachments = request.Attachments,
                SubmissionAttachmentsUid = request.SubmissionAttachmentsUid,
                Status = TaskSubmissionStatus.SentForEvaluation,
                AuthorId = request.AuthorId,
                ClubTaskId = request.ClubTaskId,
                Created = DateTime.Now
            };
            return _mapper.Map<TaskSubmissionViewModel>(submission);
        }
    }
}
