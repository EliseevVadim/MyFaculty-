using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest<CommentViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
        public int? ParentCommentId { get; set; }
        public string TextContent { get; set; }
        public string Attachments { get; set; }
    }
}
