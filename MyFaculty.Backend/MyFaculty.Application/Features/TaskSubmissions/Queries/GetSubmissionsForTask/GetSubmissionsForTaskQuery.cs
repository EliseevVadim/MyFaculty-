using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissions.Queries.GetSubmissionsForTask
{
    public class GetSubmissionsForTaskQuery : IRequest<TaskSubmissionsListViewModel>
    {
        public int IssuerId { get; set; }
        public int ClubTaskId { get; set; }
    }
}
