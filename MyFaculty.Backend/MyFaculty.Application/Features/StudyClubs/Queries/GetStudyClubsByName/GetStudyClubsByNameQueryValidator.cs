using FluentValidation;

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubsByName
{
    public class GetStudyClubsByNameQueryValidator : AbstractValidator<GetStudyClubsByNameQuery>
    {
        public GetStudyClubsByNameQueryValidator()
        {
            RuleFor(query => query.SearchRequest).NotEmpty();
        }
    }
}
