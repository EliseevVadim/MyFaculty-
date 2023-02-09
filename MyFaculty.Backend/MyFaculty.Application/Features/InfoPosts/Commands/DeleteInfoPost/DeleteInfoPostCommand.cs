using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Commands.DeleteInfoPost
{
    public class DeleteInfoPostCommand : IRequest<InfoPostViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
    }
}
