using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissionReviews.Commands.UpdateTaskSubmissionReview
{
    public class UpdateTaskSubmissionReviewCommandHandler : IRequestHandler<UpdateTaskSubmissionReviewCommand, TaskSubmissionReviewViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTaskSubmissionReviewCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskSubmissionReviewViewModel> Handle(UpdateTaskSubmissionReviewCommand request, CancellationToken cancellationToken)
        {
            AppUser reviewer = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == request.IssuerId, cancellationToken);
            if (reviewer == null)
                throw new EntityNotFoundException(nameof(AppUser), request.Id);
            TaskSubmissionReview updatingReview = await _context.TaskSubmissionReviews
                .Include(review => review.TaskSubmission)
                .FirstOrDefaultAsync(review => review.Id == request.Id, cancellationToken);
            if (updatingReview == null)
                throw new EntityNotFoundException(nameof(TaskSubmissionReview), request.Id);
            if (updatingReview.ReviewerId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            DateTime actionDate = DateTime.Now;
            updatingReview.TextContent = request.TextContent;
            updatingReview.Attachments = request.Attachments;
            updatingReview.Rate = request.Rate;
            updatingReview.Updated = actionDate;
            updatingReview.TaskSubmission.Status = request.NewStatus;
            Notification notification = new Notification()
            {
                UserId = updatingReview.TaskSubmission.AuthorId,
                TextContent = $"{string.Join(" ", new string[] { reviewer.FirstName, reviewer.LastName })} обновил отзыв на Ваше решение...",
                ReturnUrl = $"/task{updatingReview.TaskSubmission.ClubTaskId}",
                Created = actionDate
            };
            await _context.Notifications.AddAsync(notification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TaskSubmissionReviewViewModel>(updatingReview);
        }
    }
}
