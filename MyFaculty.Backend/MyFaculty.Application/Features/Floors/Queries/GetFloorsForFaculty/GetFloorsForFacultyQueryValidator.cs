using FluentValidation;

namespace MyFaculty.Application.Features.Floors.Queries.GetFloorsForFaculty
{
    public class GetFloorsForFacultyQueryValidator : AbstractValidator<GetFloorsForFacultyQuery>
    {
        public GetFloorsForFacultyQueryValidator()
        {
            RuleFor(query => query.FacultyId).NotEmpty();
        }
    }
}
