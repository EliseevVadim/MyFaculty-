using FluentValidation;

namespace MyFaculty.Application.Features.InformationPublics.Queries.GetInformationPublicsByName
{
    public class GetInformationPublicsByNameQueryValidator : AbstractValidator<GetInformationPublicsByNameQuery>
    {
        public GetInformationPublicsByNameQueryValidator()
        {
            RuleFor(query => query.SearchRequest).NotEmpty();
        }
    }
}
