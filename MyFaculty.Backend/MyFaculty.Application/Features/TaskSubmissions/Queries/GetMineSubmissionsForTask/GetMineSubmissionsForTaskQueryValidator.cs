using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissions.Queries.GetMineSubmissionsForTask
{
    public class GetMineSubmissionsForTaskQueryValidator : AbstractValidator<GetMineSubmissionsForTaskQuery>
    {
        public GetMineSubmissionsForTaskQueryValidator()
        {
            RuleFor(query => query.IssuerId).NotEmpty();
            RuleFor(query => query.ClubTaskId).NotEmpty();
        }
    }
}
