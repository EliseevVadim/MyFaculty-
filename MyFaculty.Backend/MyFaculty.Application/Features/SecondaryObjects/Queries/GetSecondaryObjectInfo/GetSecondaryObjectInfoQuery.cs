using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.SecondaryObjects.Queries.GetSecondaryObjectInfo
{
    public class GetSecondaryObjectInfoQuery : IRequest<SecondaryObjectViewModel>
    {
        public int Id { get; set; }
    }
}
