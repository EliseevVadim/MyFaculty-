using FluentValidation;

namespace MyFaculty.Application.Features.SecondaryObjects.Queries.GetSecondaryObjectInfo
{
    public class GetSecondaryObjectInfoQueryValidator : AbstractValidator<GetSecondaryObjectInfoQuery>
    {
        public GetSecondaryObjectInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
