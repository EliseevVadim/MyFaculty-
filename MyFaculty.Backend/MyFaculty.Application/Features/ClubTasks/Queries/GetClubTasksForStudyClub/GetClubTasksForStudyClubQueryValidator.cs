using FluentValidation;

namespace MyFaculty.Application.Features.ClubTasks.Queries.GetClubTasksForStudyClub
{
    public class GetClubTasksForStudyClubQueryValidator : AbstractValidator<GetClubTasksForStudyClubQuery>
    {
        public GetClubTasksForStudyClubQueryValidator()
        {
            RuleFor(query => query.StudyClubId).NotEmpty();
        }
    }
}
