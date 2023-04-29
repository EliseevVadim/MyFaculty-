using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Teachers.Queries.GetTeacherInfo
{
    public class GetTeacherInfoQuery : IRequest<TeacherViewModel>
    {
        public int Id { get; set; }
    }
}
