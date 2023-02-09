using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ClubTasks.Queries.GetClubTaskInfo
{
    public class GetClubTaskInfoQuery : IRequest<ClubTaskViewModel>
    {
        public int Id { get; set; }
    }
}
