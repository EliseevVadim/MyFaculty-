using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class FloorsListViewModel
    {
        public IList<FloorLookupDto> Floors { get; set; }
    }
}
