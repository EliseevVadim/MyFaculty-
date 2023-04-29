using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class GroupsListViewModel
    {
        public IList<GroupLookupDto> Groups { get; set; }
    }
}
