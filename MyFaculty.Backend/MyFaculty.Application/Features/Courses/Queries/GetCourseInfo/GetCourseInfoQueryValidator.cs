using FluentValidation;

namespace MyFaculty.Application.Features.Courses.Queries.GetCourseInfo
{
    public class GetCourseInfoQueryValidator : AbstractValidator<GetCourseInfoQuery>
    {
        public GetCourseInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
