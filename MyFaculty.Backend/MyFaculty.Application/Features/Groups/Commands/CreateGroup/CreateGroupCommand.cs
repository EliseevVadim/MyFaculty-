using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Groups.Commands.CreateGroup
{
    public class CreateGroupCommand : IRequest<GroupViewModel>
    {
        public string GroupName { get; set; }
        public int CourseId { get; set; }
    }
}
