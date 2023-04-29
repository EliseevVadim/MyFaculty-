using FluentValidation;

namespace MyFaculty.Application.Features.Users.Queries.GetUserInfo
{
    public class GetUserInfoQueryValidator : AbstractValidator<GetUserInfoQuery>
    {
        public GetUserInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
