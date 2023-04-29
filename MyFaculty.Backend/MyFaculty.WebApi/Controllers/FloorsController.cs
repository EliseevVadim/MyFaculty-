using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Floors.Commands.CreateFloor;
using MyFaculty.Application.Features.Floors.Commands.DeleteFloor;
using MyFaculty.Application.Features.Floors.Commands.UpdateFloor;
using MyFaculty.Application.Features.Floors.Queries.GetFloorInfo;
using MyFaculty.Application.Features.Floors.Queries.GetFloors;
using MyFaculty.Application.Features.Floors.Queries.GetFloorsForFaculty;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FloorsController : BaseController
    {
        private readonly IMapper _mapper;

        public FloorsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список этажей
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /floors
        /// </remarks>
        /// <returns>Возвращает объект FloorsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<FloorsListViewModel>> GetAll()
        {
            GetFloorsQuery query = new GetFloorsQuery();
            FloorsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информацию об этаже по id
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /floors/1
        /// </remarks>
        /// <param name="id">id этажа (integer)</param>
        /// <returns>>Возвращает объект FloorViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Этаж не найден</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FloorViewModel>> Get(int id)
        {
            GetFloorInfoQuery query = new GetFloorInfoQuery()
            {
                Id = id
            };
            FloorViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список этажей, принадлежащих указанному факультету
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /floors/faculty/1
        /// </remarks>
        /// <param name="id">id факультета (integer)</param>
        /// <returns>Возвращает объект FloorsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("faculty/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FloorsListViewModel>> GetByFacultyId(int id)
        {
            GetFloorsForFacultyQuery query = new GetFloorsForFacultyQuery()
            {
                FacultyId = id
            };
            FloorsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новую запись об этаже
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /floors
        /// {
        ///     "name": "string",
        ///     "bounds": "json"
        /// }
        /// </remarks>
        /// <param name="createFloorDto">Объект CreateFloorDto</param>
        /// <returns>Возвращает объект FloorViewModel</returns>
        /// <response code="201">Запись об этаже успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FloorViewModel>> Create([FromBody] CreateFloorDto createFloorDto)
        {
            CreateFloorCommand command = _mapper.Map<CreateFloorCommand>(createFloorDto);
            FloorViewModel floor = await Mediator.Send(command);
            return Created(nameof(FloorsController), floor);
        }

        /// <summary>
        /// Редактирует информацию об этаже
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /floors
        /// {
        ///     "id": 1,
        ///     "name": "string",
        ///     "bounds": "json"
        /// }
        /// </remarks>
        /// <param name="updateFloorDto">Объект UpdateFloorDto</param>
        /// <returns>Возвращает объект FloorViewModel</returns>
        /// <response code="200">Информация об этаже успешно обновлена</response>
        /// <response code="404">Этаж не найден</response>
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
        public async Task<ActionResult<FloorViewModel>> Update([FromBody] UpdateFloorDto updateFloorDto)
        {
            UpdateFloorCommand command = _mapper.Map<UpdateFloorCommand>(updateFloorDto);
            FloorViewModel floor = await Mediator.Send(command);
            return Ok(floor);
        }

        /// <summary>
        /// Удаляет информацию об этаже по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /floors/1
        /// </remarks>
        /// <param name="id">id этажа (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Информация об этаже успешно удалена</response>
        /// <response code="404">Этаж не найден</response>
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
            DeleteFloorCommand command = new DeleteFloorCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
