using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Floors.Queries.GetFloorsForFaculty
{
    public class GetFloorsForFacultyQuery : IRequest<FloorsListViewModel>
    {
        public int FacultyId { get; set; }
    }
}
