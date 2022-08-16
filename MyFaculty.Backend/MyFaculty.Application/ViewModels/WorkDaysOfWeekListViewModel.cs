using MyFaculty.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels
{
    public class WorkDaysOfWeekListViewModel
    {
        public IList<WorkDayOfWeekLookupDto> WorkDaysOfWeek { get; set; }
    }
}
