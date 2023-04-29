using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using MyFaculty.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissions.Commands.UpdateTaskSubmission
{
    public class UpdateTaskSubmissionCommandHandler : IRequestHandler<UpdateTaskSubmissionCommand, TaskSubmissionViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public UpdateTaskSubmissionCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskSubmissionViewModel> Handle(UpdateTaskSubmissionCommand request, CancellationToken cancellationToken)
        {
            TaskSubmission updatingSubmission = await _context.TaskSubmissions.FirstOrDefaultAsync(submission => submission.Id == request.Id);
            if (updatingSubmission == null)
                throw new EntityNotFoundException(nameof(TaskSubmission), request.Id);
            if (updatingSubmission.AuthorId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            if (updatingSubmission.Status != TaskSubmissionStatus.SentForEvaluation)
                throw new DestructiveActionException("Вы не можете редактировать это решение, поскольку оно уже оценено. Если это возможно, отправьте новое.");
            updatingSubmission.Title = request.Title;
            updatingSubmission.TextContent = request.TextContent;
            updatingSubmission.Attachments = request.Attachments;
            updatingSubmission.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TaskSubmissionViewModel>(updatingSubmission);
        }
    }
}
