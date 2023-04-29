using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.TaskSubmissions.Queries.GetSubmissionInfo
{
    public class GetSubmissionInfoQuery : IRequest<TaskSubmissionViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
    }
}
