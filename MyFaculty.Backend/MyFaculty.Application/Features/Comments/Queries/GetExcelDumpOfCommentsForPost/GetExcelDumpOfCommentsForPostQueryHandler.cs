using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Comments.Queries.GetExcelDumpOfCommentsForPost
{
    public class GetExcelDumpOfCommentsForPostQueryHandler : IRequestHandler<GetExcelDumpOfCommentsForPostQuery, ExcelFileInfoDto>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetExcelDumpOfCommentsForPostQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExcelFileInfoDto> Handle(GetExcelDumpOfCommentsForPostQuery request, CancellationToken cancellationToken)
        {
            InfoPost owningPost = await _context.InfoPosts
                .Include(post => post.OwningStudyClub)
                    .ThenInclude(club => club.Moderators)
                .FirstOrDefaultAsync(post => post.Id == request.PostId);
            if (owningPost == null)
                throw new EntityNotFoundException(nameof(InfoPost), request.PostId);
            if (!owningPost.OwningStudyClub.Moderators.Any(user => user.Id == request.IssuerId))
                throw new UnauthorizedActionException("Вы не можете выгрузить комментарии к этому посту, так как не являетесь модератором в сообществе, содержащем него");
            var comments = await _context.Comments
                .Where(comment => comment.PostId == request.PostId)
                .OrderByDescending(comment => comment.Created)
                .ProjectTo<CommentForExcelViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            if (comments.Count == 0)
                throw new EntityNotFoundException("Данные не найдены");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage package = new ExcelPackage();
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Комментарии к посту");
            worksheet.Cells["A1"].LoadFromCollection(comments, true, TableStyles.Light1);
            byte[] fileContent = package.GetAsByteArray();
            return new ExcelFileInfoDto()
            {
                Content = fileContent
            };
        }
    }
}
