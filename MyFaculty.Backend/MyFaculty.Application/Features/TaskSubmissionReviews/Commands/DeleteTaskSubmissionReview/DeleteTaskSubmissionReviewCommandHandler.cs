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

namespace MyFaculty.Application.Features.TaskSubmissionReviews.Commands.DeleteTaskSubmissionReview
{
    public class DeleteTaskSubmissionReviewCommandHandler : IRequestHandler<DeleteTaskSubmissionReviewCommand, TaskSubmissionReviewViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public DeleteTaskSubmissionReviewCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskSubmissionReviewViewModel> Handle(DeleteTaskSubmissionReviewCommand request, CancellationToken cancellationToken)
        {
            AppUser reviewer = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == request.IssuerId, cancellationToken);
            if (reviewer == null)
                throw new EntityNotFoundException(nameof(AppUser), request.Id);
            TaskSubmissionReview deletingReview = await _context.TaskSubmissionReviews
                .Include(review => review.TaskSubmission)
                .FirstOrDefaultAsync(review => review.Id == request.Id);
            if (deletingReview == null)
                throw new EntityNotFoundException(nameof(TaskSubmissionReview), request.Id);
            if (deletingReview.ReviewerId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            TaskSubmission owningSubmission = await _context.TaskSubmissions.FindAsync(new object[] { deletingReview.SubmissionId }, cancellationToken);
            owningSubmission.Status = TaskSubmissionStatus.SentForEvaluation;
            _context.TaskSubmissionReviews.Remove(deletingReview);
            Notification notification = new Notification()
            {
                UserId = deletingReview.TaskSubmission.AuthorId,
                TextContent = $"{string.Join(" ", new string[] { reviewer.FirstName, reviewer.LastName })} удалил отзыв на Ваше решение...",
                ReturnUrl = $"/task{deletingReview.TaskSubmission.ClubTaskId}",
                Created = DateTime.Now
            };
            await _context.Notifications.AddAsync(notification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TaskSubmissionReviewViewModel>(deletingReview);
        }
    }
}
