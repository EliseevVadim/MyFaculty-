using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class UsersListViewModel
    {
        public IList<UserLookupDto> Users { get; set; }
    }
}
