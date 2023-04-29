using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.InformationPublics.Queries.GetInformationPublics
{
    public class GetInformationPublicsQuery : IRequest<InformationPublicsListViewModel>
    {

    }
}
