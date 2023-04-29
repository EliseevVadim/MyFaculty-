using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Auditoriums.Commands.CreateAuditorium
{
    public class CreateAuditoriumCommand : IRequest<AuditoriumViewModel>
    {
        public string AuditoriumName { get; set; }
        public string PositionInfo { get; set; }
        public int FloorId { get; set; }
        public int TeacherId { get; set; }
    }
}
