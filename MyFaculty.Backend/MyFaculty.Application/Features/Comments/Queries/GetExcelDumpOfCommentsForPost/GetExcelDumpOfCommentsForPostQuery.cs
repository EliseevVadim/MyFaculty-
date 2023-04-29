using MediatR;
using MyFaculty.Application.Dto;

namespace MyFaculty.Application.Features.Comments.Queries.GetExcelDumpOfCommentsForPost
{
    public class GetExcelDumpOfCommentsForPostQuery : IRequest<ExcelFileInfoDto>
    {
        public int PostId { get; set; }
        public int IssuerId { get; set; }
    }
}
