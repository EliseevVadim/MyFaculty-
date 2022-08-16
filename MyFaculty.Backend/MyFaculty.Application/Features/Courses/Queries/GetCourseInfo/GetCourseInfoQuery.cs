using MediatR;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Courses.Queries.GetCourseInfo
{
    public class GetCourseInfoQuery : IRequest<CourseViewModel>
    {
        public int Id { get; set; }
    }
}
