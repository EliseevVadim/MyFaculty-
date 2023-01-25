using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
