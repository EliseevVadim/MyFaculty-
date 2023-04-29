using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroupsForFaculty
{
    public class GetGroupsForFacultyQuery : IRequest<GroupsListViewModel>
    {
        public int FacultyId { get; set; }
    }
}
