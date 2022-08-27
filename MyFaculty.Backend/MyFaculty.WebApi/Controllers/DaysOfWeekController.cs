using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.WorkDaysOfWeek.Queries.GetWorkDaysOfWeek;
using MyFaculty.Application.ViewModels;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DaysOfWeekController : BaseController
    {
        private IMapper _mapper;

        public DaysOfWeekController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of work days of week
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /daysofweek
        /// </remarks>
        /// <returns>Returns WorkDaysOfWeekListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<WorkDaysOfWeekListViewModel>> GetAll()
        {
            GetWorkDaysOfWeekQuery query = new GetWorkDaysOfWeekQuery();
            WorkDaysOfWeekListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }
    }
}
