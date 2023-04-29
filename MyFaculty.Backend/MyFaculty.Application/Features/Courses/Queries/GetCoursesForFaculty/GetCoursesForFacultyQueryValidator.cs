using FluentValidation;

namespace MyFaculty.Application.Features.Courses.Queries.GetCoursesForFaculty
{
    public class GetCoursesForFacultyQueryValidator : AbstractValidator<GetCoursesForFacultyQuery>
    {
        public GetCoursesForFacultyQueryValidator()
        {
            RuleFor(query => query.FacultyId).NotEmpty();
        }
    }
}
