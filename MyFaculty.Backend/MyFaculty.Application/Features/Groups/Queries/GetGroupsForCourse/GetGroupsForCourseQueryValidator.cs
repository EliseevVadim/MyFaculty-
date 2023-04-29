using FluentValidation;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroupsForCourse
{
    public class GetGroupsForCourseQueryValidator : AbstractValidator<GetGroupsForCourseQuery>
    {
        public GetGroupsForCourseQueryValidator()
        {
            RuleFor(query => query.CourseId).NotEmpty();
        }
    }
}
