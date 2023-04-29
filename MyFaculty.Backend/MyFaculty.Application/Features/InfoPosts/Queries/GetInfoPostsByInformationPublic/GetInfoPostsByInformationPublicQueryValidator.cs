using FluentValidation;

namespace MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPostsByInformationPublic
{
    public class GetInfoPostsByInformationPublicQueryValidator : AbstractValidator<GetInfoPostsByInformationPublicQuery>
    {
        public GetInfoPostsByInformationPublicQueryValidator()
        {
            RuleFor(query => query.PublicId).NotEmpty();
            RuleFor(query => query.IssuerId).NotEmpty();
        }
    }
}
