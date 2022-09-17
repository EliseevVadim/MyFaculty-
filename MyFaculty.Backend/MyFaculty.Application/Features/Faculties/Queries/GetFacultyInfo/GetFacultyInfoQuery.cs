using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Faculties.Queries.GetFacultyInfo
{
    public class GetFacultyInfoQuery : IRequest<FacultyViewModel>
    {
        public int Id { get; set; }
    }
}
