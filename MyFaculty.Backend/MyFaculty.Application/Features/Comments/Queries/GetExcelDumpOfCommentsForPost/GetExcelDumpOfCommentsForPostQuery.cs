using MediatR;
using MyFaculty.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Comments.Queries.GetExcelDumpOfCommentsForPost
{
    public class GetExcelDumpOfCommentsForPostQuery : IRequest<ExcelFileInfoDto>
    {
        public int PostId { get; set; }
        public int IssuerId { get; set; }
    }
}
