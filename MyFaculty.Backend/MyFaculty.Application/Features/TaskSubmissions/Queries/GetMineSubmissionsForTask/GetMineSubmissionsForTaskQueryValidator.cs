using FluentValidation;

namespace MyFaculty.Application.Features.TaskSubmissions.Queries.GetMineSubmissionsForTask
{
    public class GetMineSubmissionsForTaskQueryValidator : AbstractValidator<GetMineSubmissionsForTaskQuery>
    {
        public GetMineSubmissionsForTaskQueryValidator()
        {
            RuleFor(query => query.IssuerId).NotEmpty();
            RuleFor(query => query.ClubTaskId).NotEmpty();
        }
    }
}
