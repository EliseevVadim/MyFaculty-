using FluentValidation;

namespace MyFaculty.Application.Features.Faculties.Queries.GetFacultyInfo
{
    public class GetFacultyInfoQueryValidator : AbstractValidator<GetFacultyInfoQuery>
    {
        public GetFacultyInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
