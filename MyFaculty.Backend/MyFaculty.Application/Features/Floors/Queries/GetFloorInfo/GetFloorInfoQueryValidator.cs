using FluentValidation;

namespace MyFaculty.Application.Features.Floors.Queries.GetFloorInfo
{
    public class GetFloorInfoQueryValidator : AbstractValidator<GetFloorInfoQuery>
    {
        public GetFloorInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
