using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.WorkDaysOfWeek.Queries.GetWorkDaysOfWeek;
using MyFaculty.Application.ViewModels;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class DaysOfWeekController : BaseController
    {
        private IMapper _mapper;

        public DaysOfWeekController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<WorkDaysOfWeekListViewModel>> GetAll()
        {
            GetWorkDaysOfWeekQuery query = new GetWorkDaysOfWeekQuery();
            WorkDaysOfWeekListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }
    }
}
