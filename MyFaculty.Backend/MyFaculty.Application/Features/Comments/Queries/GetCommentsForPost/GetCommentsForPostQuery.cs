using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Comments.Queries.GetCommentsForPost
{
    public class GetCommentsForPostQuery : IRequest<CommentsListViewModel>
    {
        public int PostId { get; set; }
        public int IssuerId { get; set; }
    }
}
