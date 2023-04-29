using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.InformationPublics.Queries.GetInformationPublicInfo
{
    public class GetInformationPublicInfoQuery : IRequest<InformationPublicViewModel>
    {
        public int Id { get; set; }
    }
}
