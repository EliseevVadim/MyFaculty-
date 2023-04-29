using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Pairs.Commands.CreatePair;
using MyFaculty.Application.Features.Pairs.Commands.DeletePair;
using MyFaculty.Application.Features.Pairs.Commands.UpdatePair;
using MyFaculty.Application.Features.Pairs.Queries.GetPairInfo;
using MyFaculty.Application.Features.Pairs.Queries.GetPairs;
using MyFaculty.Application.Features.Pairs.Queries.GetPairsForGroup;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PairsController : BaseController
    {
        private IMapper _mapper;

        public PairsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список пар
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /pairs
        /// </remarks>
        /// <returns>Возвращает объект PairsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PairsListViewModel>> GetAll()
        {
            GetPairsQuery query = new GetPairsQuery();
            PairsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информацию о паре по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /pairs/1
        /// </remarks>
        /// <param name="id">id пары (integer)</param>
        /// <returns>Возвращает объект PairViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Пара не найдена</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PairViewModel>> Get(int id)
        {
            GetPairInfoQuery query = new GetPairInfoQuery()
            {
                Id = id
            };
            PairViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список пар для конкретной группы
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /pairs/group/1
        /// </remarks>
        /// <param name="id">id группы (integer)</param>
        /// <returns>Возвращает объект PairsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("group/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PairsListViewModel>> GetByGroupId(int id)
        {
            GetPairsForGroupQuery query = new GetPairsForGroupQuery()
            {
                GroupId = id
            };
            PairsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новую запись о паре
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /pairs
        /// {
        ///     "pairName": "string",
        ///     "teacherId": 1,
        ///     "auditoriumId": 1,
        ///     "groupId": 1,
        ///     "disciplineId": 1,
        ///     "dayOfWeekId": 1,
        ///     "pairRepeatingId": 1,
        ///     "pairInfoId": 1
        /// }
        /// </remarks>
        /// <param name="createPairDto">Объект CreatePairDto</param>
        /// <returns>Возвращает объект PairViewModel</returns>
        /// <response code="201">Запись о паре успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PairViewModel>> Create([FromBody] CreatePairDto createPairDto)
        {
            CreatePairCommand command = _mapper.Map<CreatePairCommand>(createPairDto);
            PairViewModel pair = await Mediator.Send(command);
            return Created(nameof(PairsController), pair);
        }

        /// <summary>
        /// Редактирует информацию о паре
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /pairs
        /// {
        ///     "pairName": "string",
        ///     "teacherId": 1,
        ///     "auditoriumId": 1,
        ///     "groupId": 1,
        ///     "disciplineId": 1,
        ///     "dayOfWeekId": 1,
        ///     "pairRepeatingId": 1,
        ///     "pairInfoId": 1
        /// }
        /// </remarks>
        /// <param name="updatePairDto">Объект UpdatePairDto</param>
        /// <returns>Возвращает объект PairViewModel</returns>
        /// <response code="200">Информация о паре успешно обновлена</response>
        /// <response code="404">Пара не найдена</response>
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
        public async Task<ActionResult<PairViewModel>> Update([FromBody] UpdatePairDto updatePairDto)
        {
            UpdatePairCommand command = _mapper.Map<UpdatePairCommand>(updatePairDto);
            PairViewModel pair = await Mediator.Send(command);
            return Ok(pair);
        }

        /// <summary>
        /// Удаляет информацию о паре по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /pairs/1
        /// </remarks>
        /// <param name="id">id пары (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Информация о паре успешно удалена</response>
        /// <response code="404">Пара не найдена</response>
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
            DeletePairCommand command = new DeletePairCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
