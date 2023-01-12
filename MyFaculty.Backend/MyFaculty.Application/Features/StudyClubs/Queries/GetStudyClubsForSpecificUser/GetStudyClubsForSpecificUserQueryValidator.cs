using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubsForSpecificUser
{
    public class GetStudyClubsForSpecificUserQueryValidator : AbstractValidator<GetStudyClubsForSpecificUserQuery>
    {
        public GetStudyClubsForSpecificUserQueryValidator()
        {
            RuleFor(query => query.UserId).NotEmpty();
        }
    }
}
