using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissions.Queries.GetSubmissionInfo
{
    public class GetSubmissionInfoQuery : IRequest<TaskSubmissionViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
    }
}
