using FluentValidation;

namespace MyFaculty.Application.Features.Notifications.Queries.GetNotificationsForUser
{
    public class GetNotificationsForUserQueryValidator : AbstractValidator<GetNotificationsForUserQuery>
    {
        public GetNotificationsForUserQueryValidator()
        {
            RuleFor(query => query.UserId).NotEmpty();
        }
    }
}
