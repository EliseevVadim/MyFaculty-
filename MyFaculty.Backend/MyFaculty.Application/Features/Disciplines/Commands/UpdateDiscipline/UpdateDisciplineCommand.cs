using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Disciplines.Commands.UpdateDiscipline
{
    public class UpdateDisciplineCommand : IRequest<DisciplineViewModel>
    {
        public int Id { get; set; }
        public string DisciplineName { get; set; }
    }
}
