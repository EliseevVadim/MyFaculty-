using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Comments.Queries.GetExcelDumpOfCommentsForPost
{
    public class GetExcelDumpOfCommentsForPostQueryValidator : AbstractValidator<GetExcelDumpOfCommentsForPostQuery>
    {
        public GetExcelDumpOfCommentsForPostQueryValidator()
        {
            RuleFor(query => query.PostId).NotEmpty();
            RuleFor(query => query.IssuerId).NotEmpty();
        }
    }
}
