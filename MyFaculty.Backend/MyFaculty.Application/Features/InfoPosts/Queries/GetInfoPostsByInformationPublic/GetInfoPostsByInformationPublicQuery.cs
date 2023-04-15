using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPostsByInformationPublic
{
    public class GetInfoPostsByInformationPublicQuery : IRequest<InfoPostsListViewModel>
    {
        public int PublicId { get; set; }
        public int IssuerId { get; set; }
    }
}
