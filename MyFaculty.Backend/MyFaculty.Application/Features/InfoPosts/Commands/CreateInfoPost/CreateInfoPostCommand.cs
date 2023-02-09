using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Commands.CreateInfoPost
{
    public class CreateInfoPostCommand : IRequest<InfoPostViewModel>
    {
        public string TextContent { get; set; }
        public string Attachments { get; set; }
        public Guid PostAttachmentsUid { get; set; }
        public int? StudyClubId { get; set; }
        public int? InfoPublicId { get; set; }
        public int AuthorId { get; set; }
        public bool CommentsAllowed { get; set; }
    }
}
