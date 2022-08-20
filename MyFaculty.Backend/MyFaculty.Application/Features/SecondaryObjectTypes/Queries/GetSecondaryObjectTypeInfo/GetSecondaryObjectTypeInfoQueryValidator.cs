using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Queries.GetSecondaryObjectTypeInfo
{
    public class GetSecondaryObjectTypeInfoQueryValidator : AbstractValidator<GetSecondaryObjectTypeInfoQuery>
    {
        public GetSecondaryObjectTypeInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
