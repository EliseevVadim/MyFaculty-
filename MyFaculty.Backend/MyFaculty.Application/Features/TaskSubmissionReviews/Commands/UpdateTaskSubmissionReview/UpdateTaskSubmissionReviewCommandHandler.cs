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

namespace MyFaculty.Application.Features.TaskSubmissionReviews.Commands.UpdateTaskSubmissionReview
{
    public class UpdateTaskSubmissionReviewCommandHandler : IRequestHandler<UpdateTaskSubmissionReviewCommand, TaskSubmissionReviewViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public UpdateTaskSubmissionReviewCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskSubmissionReviewViewModel> Handle(UpdateTaskSubmissionReviewCommand request, CancellationToken cancellationToken)
        {
            TaskSubmissionReview updatingReview = await _context.TaskSubmissionReviews
                .Include(review => review.TaskSubmission)
                .FirstOrDefaultAsync(review => review.Id == request.Id, cancellationToken);
            if (updatingReview == null)
                throw new EntityNotFoundException(nameof(TaskSubmissionReview), request.Id);
            if (updatingReview.ReviewerId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            updatingReview.TextContent = request.TextContent;
            updatingReview.Attachments = request.Attachments;
            updatingReview.Rate = request.Rate;
            updatingReview.Updated = DateTime.Now;
            updatingReview.TaskSubmission.Status = request.NewStatus;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TaskSubmissionReviewViewModel>(updatingReview);
        }
    }
}
