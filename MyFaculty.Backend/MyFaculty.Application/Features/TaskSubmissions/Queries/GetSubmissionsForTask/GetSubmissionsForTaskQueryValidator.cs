using FluentValidation;

namespace MyFaculty.Application.Features.TaskSubmissions.Queries.GetSubmissionsForTask
{
    public class GetSubmissionsForTaskQueryValidator : AbstractValidator<GetSubmissionsForTaskQuery>
    {
        public GetSubmissionsForTaskQueryValidator()
        {
            RuleFor(query => query.IssuerId).NotEmpty();
            RuleFor(query => query.ClubTaskId).NotEmpty();
        }
    }
}
