using FluentValidation;

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
