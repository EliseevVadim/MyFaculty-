using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroupInfo
{
    public class GetGroupInfoQuery : IRequest<GroupViewModel>
    {
        public int Id { get; set; }
    }
}
