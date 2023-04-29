using FluentValidation;

namespace MyFaculty.Application.Features.TeachersDisciplines.Queries.GetTeacherDisciplineInfo
{
    public class GetTeacherDisciplineInfoQueryValidator : AbstractValidator<GetTeacherDisciplineInfoQuery>
    {
        public GetTeacherDisciplineInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
