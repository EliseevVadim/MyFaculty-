using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Newsfeed.Queries.GeneratePostsNewsfeedForUser
{
    public class GeneratePostsNewsfeedForUserQuery : IRequest<InfoPostsListViewModel>
    {
        public int UserId { get; set; }
    }
}
