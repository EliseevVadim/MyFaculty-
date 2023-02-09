using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ClubTasks.Queries.GetClubTaskInfo
{
    public class GetClubTaskInfoQueryValidator : AbstractValidator<GetClubTaskInfoQuery>
    {
        public GetClubTaskInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
