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
        /// Возвращает список рабочих дней недели
        /// </summary>
        /// <remarks>
        /// Пример запроса:  
        /// GET /daysofweek
        /// </remarks>
        /// <returns>Возвращает объект WorkDaysOfWeekListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
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
