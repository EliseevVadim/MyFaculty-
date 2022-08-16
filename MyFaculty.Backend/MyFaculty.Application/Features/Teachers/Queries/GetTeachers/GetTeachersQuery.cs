using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Teachers.Queries.GetTeachers
{
    public class GetTeachersQuery : IRequest<TeachersListViewModel>
    {

    }
}
