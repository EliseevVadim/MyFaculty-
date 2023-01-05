using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroupsForCourse
{
    public class GetGroupsForCourseQuery : IRequest<GroupsListViewModel>
    {
        public int CourseId { get; set; }
    }
}
