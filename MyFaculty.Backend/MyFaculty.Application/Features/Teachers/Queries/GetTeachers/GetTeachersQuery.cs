using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Teachers.Queries.GetTeachers
{
    public class GetTeachersQuery : IRequest<TeachersListViewModel>
    {

    }
}
