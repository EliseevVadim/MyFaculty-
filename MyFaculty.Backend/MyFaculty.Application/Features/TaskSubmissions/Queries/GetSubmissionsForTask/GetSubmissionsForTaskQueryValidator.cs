using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissions.Queries.GetSubmissionsForTask
{
    public class GetSubmissionsForTaskQueryValidator : AbstractValidator<GetSubmissionsForTaskQuery>
    {
        public GetSubmissionsForTaskQueryValidator()
        {
            RuleFor(query => query.IssuerId).NotEmpty();
            RuleFor(query => query.ClubTaskId).NotEmpty();
        }
    }
}
