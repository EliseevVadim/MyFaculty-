using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Auditoriums.Commands.UpdateAuditorium
{
    public class UpdateAuditoriumCommand : IRequest<AuditoriumViewModel>
    {
        public int Id { get; set; }
        public string AuditoriumName { get; set; }
        public string PositionInfo { get; set; }
        public int FloorId { get; set; }
        public int TeacherId { get; set; }
    }
}
