using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
