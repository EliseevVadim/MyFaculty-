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

namespace MyFaculty.Application.Features.TaskSubmissionReviews.Commands.CreateTaskSubmissionReview
{
    public class CreateTaskSubmissionReviewCommandHandler : IRequestHandler<CreateTaskSubmissionReviewCommand, TaskSubmissionReviewViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreateTaskSubmissionReviewCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskSubmissionReviewViewModel> Handle(CreateTaskSubmissionReviewCommand request, CancellationToken cancellationToken)
        {
            AppUser reviewer = await _context.Users
                .Include(user => user.StudyClubsAtModeration)
                .FirstOrDefaultAsync(user => user.Id == request.ReviewerId, cancellationToken);
            if (reviewer == null)
                throw new EntityNotFoundException(nameof(AppUser), request.ReviewerId);
            TaskSubmission reviewingSubmission = await _context.TaskSubmissions
                .Include(sumbmission => sumbmission.ClubTask)
                    .ThenInclude(task => task.OwningStudyClub)
                .FirstOrDefaultAsync(sumbmission => sumbmission.Id == request.SubmissionId, cancellationToken);
            if (reviewingSubmission == null)
                throw new EntityNotFoundException(nameof(TaskSubmission), request.SubmissionId);
            if (!reviewer.StudyClubsAtModeration.Contains(reviewingSubmission.ClubTask.OwningStudyClub))
                throw new UnauthorizedActionException("Вы не можете оценивать решение этого задания, поскольку не являетесь модератором сообщества, содержащего его.");
            if (request.Rate > reviewingSubmission.ClubTask.Cost)
                throw new DestructiveActionException("Оценка не может превышать максимальную для этого задания");
            DateTime actionDate = DateTime.Now;
            TaskSubmissionReview review = new TaskSubmissionReview()
            {
                TextContent = request.TextContent,
                SubmissionReviewAttachmentsUid = request.SubmissionReviewAttachmentsUid,
                Attachments = request.Attachments,
                Rate = request.Rate,
                ReviewerId = request.ReviewerId,
                SubmissionId = request.SubmissionId,
                Created = actionDate
            };
            reviewingSubmission.Status = request.NewStatus;
            Notification notification = new Notification()
            {
                UserId = reviewingSubmission.AuthorId,
                TextContent = $"{string.Join(" ", new string[] { reviewer.FirstName, reviewer.LastName })} дал отзыв на Ваше решение...",
                ReturnUrl = $"/task{reviewingSubmission.ClubTaskId}",
                Created = actionDate
            };
            await _context.TaskSubmissionReviews.AddAsync(review, cancellationToken);
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TaskSubmissionReviewViewModel>(review);
        }
    }
}
