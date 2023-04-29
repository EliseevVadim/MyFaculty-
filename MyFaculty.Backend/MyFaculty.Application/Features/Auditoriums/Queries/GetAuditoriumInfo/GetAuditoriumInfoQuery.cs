using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriumInfo
{
    public class GetAuditoriumInfoQuery : IRequest<AuditoriumViewModel>
    {
        public int Id { get; set; }
    }
}
