using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPostsByInformationPublic
{
    public class GetInfoPostsByInformationPublicQueryValidator : AbstractValidator<GetInfoPostsByInformationPublicQuery>
    {
        public GetInfoPostsByInformationPublicQueryValidator()
        {
            RuleFor(query => query.PublicId).NotEmpty();
        }
    }
}
