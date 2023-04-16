using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
