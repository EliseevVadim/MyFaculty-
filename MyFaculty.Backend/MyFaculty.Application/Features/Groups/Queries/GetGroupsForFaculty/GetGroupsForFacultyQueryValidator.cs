using FluentValidation;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroupsForFaculty
{
    public class GetGroupsForFacultyQueryValidator : AbstractValidator<GetGroupsForFacultyQuery>
    {
        public GetGroupsForFacultyQueryValidator()
        {
            RuleFor(query => query.FacultyId).NotEmpty();
        }
    }
}
