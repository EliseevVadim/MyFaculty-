using FluentValidation;

namespace MyFaculty.Application.Features.InformationPublics.Queries.GetInformationPublicInfo
{
    public class GetInformationPublicInfoQueryValidator : AbstractValidator<GetInformationPublicInfoQuery>
    {
        public GetInformationPublicInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
