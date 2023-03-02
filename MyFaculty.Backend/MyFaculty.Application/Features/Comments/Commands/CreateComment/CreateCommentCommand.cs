using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<CommentViewModel>
    {
        public string TextContent { get; set; }
        public string Attachments { get; set; }
        public Guid CommentAttachmentsUid { get; set; }
        public int AuthorId { get; set; }
        public int PostId { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
