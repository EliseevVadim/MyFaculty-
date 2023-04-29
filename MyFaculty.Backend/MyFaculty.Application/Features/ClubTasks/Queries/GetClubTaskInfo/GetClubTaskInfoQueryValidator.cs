using FluentValidation;

namespace MyFaculty.Application.Features.ClubTasks.Queries.GetClubTaskInfo
{
    public class GetClubTaskInfoQueryValidator : AbstractValidator<GetClubTaskInfoQuery>
    {
        public GetClubTaskInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
