using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubsByName
{
    public class GetStudyClubsByNameQuery : IRequest<StudyClubsListViewModel>
    {
        public string SearchRequest { get; set; }
    }
}
