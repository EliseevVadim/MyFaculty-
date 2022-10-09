using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriumsForFaculty
{
    public class GetAuditoriumsForFacultyQuery : IRequest<AuditoriumsListViewModel>
    {
        public int FacultyId { get; set; }
    }
}
