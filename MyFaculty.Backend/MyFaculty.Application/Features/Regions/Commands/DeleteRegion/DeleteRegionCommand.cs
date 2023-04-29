using MediatR;

namespace MyFaculty.Application.Features.Regions.Commands.DeleteRegion
{
    public class DeleteRegionCommand : IRequest
    {
        public int Id { get; set; }
    }
}
