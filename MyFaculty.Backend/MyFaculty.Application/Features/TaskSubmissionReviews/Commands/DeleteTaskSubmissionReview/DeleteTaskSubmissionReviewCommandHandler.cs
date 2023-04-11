using AutoMapper;
using MediatR;
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

namespace MyFaculty.Application.Features.TaskSubmissionReviews.Commands.DeleteTaskSubmissionReview
{
    public class DeleteTaskSubmissionReviewCommandHandler : IRequestHandler<DeleteTaskSubmissionReviewCommand, TaskSubmissionReviewViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public DeleteTaskSubmissionReviewCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskSubmissionReviewViewModel> Handle(DeleteTaskSubmissionReviewCommand request, CancellationToken cancellationToken)
        {
            TaskSubmissionReview deletingReview = await _context.TaskSubmissionReviews.FindAsync(new object[] { request.Id }, cancellationToken);
            if (deletingReview == null)
                throw new EntityNotFoundException(nameof(TaskSubmissionReview), request.Id);
            if (deletingReview.ReviewerId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            TaskSubmission owningSubmission = await _context.TaskSubmissions.FindAsync(new object[] { deletingReview.SubmissionId }, cancellationToken);
            owningSubmission.Status = TaskSubmissionStatus.SentForEvaluation;
            _context.TaskSubmissionReviews.Remove(deletingReview);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TaskSubmissionReviewViewModel>(deletingReview);
        }
    }
}
