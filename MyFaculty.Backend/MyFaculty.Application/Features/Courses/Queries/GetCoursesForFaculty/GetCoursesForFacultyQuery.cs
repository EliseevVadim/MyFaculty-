using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Courses.Queries.GetCoursesForFaculty
{
    public class GetCoursesForFacultyQuery : IRequest<CoursesListViewModel>
    {
        public int FacultyId { get; set; }
    }
}
