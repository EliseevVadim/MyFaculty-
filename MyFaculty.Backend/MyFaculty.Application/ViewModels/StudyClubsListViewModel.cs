using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class StudyClubsListViewModel
    {
        public IList<StudyClubLookupDto> StudyClubs { get; set; }
    }
}
