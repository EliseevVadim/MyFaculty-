using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Disciplines.Commands.CreateDiscipline
{
    public class CreateDisciplineCommand : IRequest<DisciplineViewModel>
    {
        public string DisciplineName { get; set; }
    }
}
