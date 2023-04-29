using FluentValidation;

namespace MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriumsForFaculty
{
    public class GetAuditoriumsForFacultyQueryValidator : AbstractValidator<GetAuditoriumsForFacultyQuery>
    {
        public GetAuditoriumsForFacultyQueryValidator()
        {
            RuleFor(query => query.FacultyId).NotEmpty();
        }
    }
}
