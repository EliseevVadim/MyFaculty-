using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFaculty.Domain.Entities;
using MediatR;

namespace MyFaculty.Application.Features.TeachersDisciplines.Commands.CreateTeacherDiscipline
{
    public class CreateTeacherDisciplineCommand : IRequest<TeacherDiscipline>
    {
        public int TeacherId { get; set; }
        public int DisciplineId { get; set; }
    }
}
