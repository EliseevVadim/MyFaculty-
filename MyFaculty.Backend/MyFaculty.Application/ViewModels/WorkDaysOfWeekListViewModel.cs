using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class WorkDaysOfWeekListViewModel
    {
        public IList<WorkDayOfWeekLookupDto> WorkDaysOfWeek { get; set; }
    }
}
