using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroupsForCourse
{
    public class GetGroupsForCourseQuery : IRequest<GroupsListViewModel>
    {
        public int CourseId { get; set; }
    }
}
