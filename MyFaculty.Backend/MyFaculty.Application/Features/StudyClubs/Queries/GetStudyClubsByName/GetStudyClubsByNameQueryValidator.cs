using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubsByName
{
    public class GetStudyClubsByNameQueryValidator : AbstractValidator<GetStudyClubsByNameQuery>
    {
        public GetStudyClubsByNameQueryValidator()
        {
            RuleFor(query => query.SearchRequest).NotEmpty();
        }
    }
}
