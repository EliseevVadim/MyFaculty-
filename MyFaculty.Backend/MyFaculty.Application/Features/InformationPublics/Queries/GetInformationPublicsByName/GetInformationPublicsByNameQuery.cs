using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.InformationPublics.Queries.GetInformationPublicsByName
{
    public class GetInformationPublicsByNameQuery : IRequest<InformationPublicsListViewModel>
    {
        public string SearchRequest { get; set; }
    }
}
