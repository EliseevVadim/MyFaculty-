using FluentValidation;

namespace MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriumInfo
{
    public class GetAuditoriumInfoQueryValidator : AbstractValidator<GetAuditoriumInfoQuery>
    {
        public GetAuditoriumInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
