using FluentValidation;

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubInfo
{
    public class GetStudyClubInfoQueryValidator : AbstractValidator<GetStudyClubInfoQuery>
    {
        public GetStudyClubInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
