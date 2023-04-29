using MediatR;
using MyFaculty.Application.ViewModels;
using System;

namespace MyFaculty.Application.Features.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommand : IRequest<TeacherViewModel>
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string PhotoPath { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int ScienceRankId { get; set; }
    }
}
