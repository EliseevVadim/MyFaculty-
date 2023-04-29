using FluentValidation;

namespace MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPostsByStudyClub
{
    public class GetInfoPostsByStudyClubQueryValidator : AbstractValidator<GetInfoPostsByStudyClubQuery>
    {
        public GetInfoPostsByStudyClubQueryValidator()
        {
            RuleFor(query => query.StudyClubId).NotEmpty();
        }
    }
}
