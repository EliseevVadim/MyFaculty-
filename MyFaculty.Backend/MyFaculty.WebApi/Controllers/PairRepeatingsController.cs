using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.PairRepeatings.Queries.GetPairRepeatings;
using MyFaculty.Application.ViewModels;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PairRepeatingsController : BaseController
    {
        private readonly IMapper _mapper;

        public PairRepeatingsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список записей о повторяемости пар
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /pairrepeatings
        /// </remarks>
        /// <returns>Возвращает объект PairRepeatingsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PairRepeatingsListViewModel>> GetAll()
        {
            GetPairRepeatingsQuery query = new GetPairRepeatingsQuery();
            PairRepeatingsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }
    }
}
