using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.TeachersDisciplines.Queries.GetTeacherDisciplineInfo
{
    public class GetTeacherDisciplineInfoQuery : IRequest<TeacherDisciplineViewModel>
    {
        public int Id { get; set; }
    }
}
