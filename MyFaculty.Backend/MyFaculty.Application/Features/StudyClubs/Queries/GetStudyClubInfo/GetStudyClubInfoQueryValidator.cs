using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubInfo
{
    public class GetStudyClubInfoQueryValidator : AbstractValidator<GetStudyClubInfoQuery>
    {
        public GetStudyClubInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
