using FluentValidation;

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubsForSpecificUser
{
    public class GetStudyClubsForSpecificUserQueryValidator : AbstractValidator<GetStudyClubsForSpecificUserQuery>
    {
        public GetStudyClubsForSpecificUserQueryValidator()
        {
            RuleFor(query => query.UserId).NotEmpty();
        }
    }
}
