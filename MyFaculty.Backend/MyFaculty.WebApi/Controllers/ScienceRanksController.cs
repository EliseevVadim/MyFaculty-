using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.ScienceRanks.Commands.CreateScienceRank;
using MyFaculty.Application.Features.ScienceRanks.Commands.DeleteScienceRank;
using MyFaculty.Application.Features.ScienceRanks.Commands.UpdateScienceRank;
using MyFaculty.Application.Features.ScienceRanks.Queries.GetScienceRankInfo;
using MyFaculty.Application.Features.ScienceRanks.Queries.GetScienceRanks;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ScienceRanksController : BaseController
    {
        private IMapper _mapper;

        public ScienceRanksController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список ученых званий
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /scienceranks
        /// </remarks>
        /// <returns>Возвращает объект ScienceRanksListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ScienceRanksListViewModel>> GetAll()
        {
            GetScienceRanksQuery query = new GetScienceRanksQuery();
            ScienceRanksListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информацию об ученом звании по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /scienceranks/1
        /// </remarks>
        /// <param name="id">id ученого звания (integer)</param>
        /// <returns>Возвращает объект ScienceRankViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Ученое звание не найдено</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ScienceRankViewModel>> Get(int id)
        {
            GetScienceRankInfoQuery query = new GetScienceRankInfoQuery()
            {
                Id = id
            };
            ScienceRankViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новую запись об ученом звании
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /scienceranks
        /// {
        ///     "rankName": "string",
        /// }
        /// </remarks>
        /// <param name="createScienceRankDto">Объект CreateScienceRankDto</param>
        /// <returns>Возвращает объект ScienceRankViewModel</returns>
        /// <response code="201">Запись об ученом звании успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ScienceRankViewModel>> Create([FromBody] CreateScienceRankDto createScienceRankDto)
        {
            CreateScienceRankCommand command = _mapper.Map<CreateScienceRankCommand>(createScienceRankDto);
            ScienceRankViewModel rank = await Mediator.Send(command);
            return Created(nameof(ScienceRanksController), rank);
        }

        /// <summary>
        /// Редактирует информацию об ученом звании
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /scienceranks
        /// {
        ///     "id": 1,
        ///     "rankName": "string",
        /// }
        /// </remarks>
        /// <param name="updateScienceRankDto">Объект UpdateScienceRankDto</param>
        /// <returns>Возвращает объект ScienceRankViewModel</returns>
        /// <response code="200">Информация об ученом звании успешно обновлена</response>
        /// <response code="404">Ученое звание не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>  
        /// <response code="500">Внутренняя серверная ошибка</response> 
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ScienceRankViewModel>> Update([FromBody] UpdateScienceRankDto updateScienceRankDto)
        {
            UpdateScienceRankCommand command = _mapper.Map<UpdateScienceRankCommand>(updateScienceRankDto);
            ScienceRankViewModel rank = await Mediator.Send(command);
            return Ok(rank);
        }

        /// <summary>
        /// Удаляет информацию об ученом звании по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /scienceranks/1
        /// </remarks>
        /// <param name="id">id ученого звания (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Информация об ученом звании удалена</response>
        /// <response code="404">Ученое звание не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteScienceRankCommand command = new DeleteScienceRankCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
