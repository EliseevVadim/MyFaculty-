using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Queries.GetSecondaryObjectTypeInfo
{
    public class GetSecondaryObjectTypeInfoQuery : IRequest<SecondaryObjectTypeViewModel>
    {
        public int Id { get; set; }
    }
}
