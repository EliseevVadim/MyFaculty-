using FluentValidation;

namespace MyFaculty.Application.Features.TaskSubmissions.Queries.GetSubmissionInfo
{
    public class GetSubmissionInfoQueryValidator : AbstractValidator<GetSubmissionInfoQuery>
    {
        public GetSubmissionInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
            RuleFor(query => query.IssuerId).NotEmpty();
        }
    }
}
