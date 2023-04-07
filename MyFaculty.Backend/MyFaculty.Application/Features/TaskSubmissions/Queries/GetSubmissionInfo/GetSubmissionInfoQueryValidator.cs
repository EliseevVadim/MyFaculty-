using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissions.Queries.GetSubmissionInfo
{
    public class GetSubmissionInfoQueryValidator : AbstractValidator<GetSubmissionInfoQuery>
    {
        public GetSubmissionInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
            RuleFor(query => query.IssuerId).NotEmpty();
        }
    }
}
