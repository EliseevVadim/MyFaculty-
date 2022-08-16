using MyFaculty.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels
{
    public class SecondaryObjectsListViewModel
    {
        public IList<SecondaryObjectLookupDto> SecondaryObjects { get; set; }
    }
}
