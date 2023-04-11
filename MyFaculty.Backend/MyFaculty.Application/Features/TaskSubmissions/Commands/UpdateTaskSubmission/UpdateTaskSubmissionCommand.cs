using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissions.Commands.UpdateTaskSubmission
{
    public class UpdateTaskSubmissionCommand : IRequest<TaskSubmissionViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
        public string Title { get; set; }
        public string TextContent { get; set; }
        public string Attachments { get; set; }
    }
}
