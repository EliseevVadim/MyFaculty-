using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.ClubTasks.Commands.DeleteClubTask
{
    public class DeleteClubTaskCommand : IRequest<ClubTaskViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
    }
}
