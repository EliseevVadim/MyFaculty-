using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Courses.Queries.GetCourses
{
    public class GetCoursesQuery : IRequest<CoursesListViewModel>
    {

    }
}
