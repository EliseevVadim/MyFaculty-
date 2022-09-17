using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Faculties.Commands.CreateFaculty
{
    public class CreateFacultyCommand : IRequest<FacultyViewModel>
    {
        public string FacultyName { get; set; }
        public string OfficialWebsite { get; set; }
    }
}
