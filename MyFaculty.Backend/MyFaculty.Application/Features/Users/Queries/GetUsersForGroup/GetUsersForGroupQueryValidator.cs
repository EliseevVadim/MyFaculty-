using FluentValidation;

namespace MyFaculty.Application.Features.Users.Queries.GetUsersForGroup
{
    public class GetUsersForGroupQueryValidator : AbstractValidator<GetUsersForGroupQuery>
    {
        public GetUsersForGroupQueryValidator()
        {
            RuleFor(query => query.GroupId).NotEmpty();
        }
    }
}
