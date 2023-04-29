using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Groups.Commands.UpdateGroup
{
    public class UpdateGroupCommand : IRequest<GroupViewModel>
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int CourseId { get; set; }
    }
}
