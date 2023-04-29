using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Auditoriums.Commands.CreateAuditorium;
using MyFaculty.Application.Features.Auditoriums.Commands.DeleteAuditorium;
using MyFaculty.Application.Features.Auditoriums.Commands.UpdateAuditorium;
using MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriumInfo;
using MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriums;
using MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriumsForFaculty;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuditoriumsController : BaseController
    {
        private readonly IMapper _mapper;

        public AuditoriumsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список аудиторий
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /auditoriums
        /// </remarks>
        /// <returns>Возвращает объект AuditoriumsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuditoriumsListViewModel>> GetAll()
        {
            GetAuditoriumsQuery query = new GetAuditoriumsQuery();
            AuditoriumsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информацию об аудитории по id
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /auditoriums/1
        /// </remarks>
        /// <param name="id">id аудитории (integer)</param>
        /// <returns>Возвращает объект AuditoriumViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Аудитория не найдена</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuditoriumViewModel>> Get(int id)
        {
            GetAuditoriumInfoQuery query = new GetAuditoriumInfoQuery()
            {
                Id = id
            };
            AuditoriumViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список аудиторий, принадлежащих указанному факультету
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /auditoriums/faculty/1
        /// </remarks>
        /// <param name="id">id факультета (integer)</param>
        /// <returns>Возвращает объект AuditoriumsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("faculty/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuditoriumsListViewModel>> GetByFacultyId(int id)
        {
            GetAuditoriumsForFacultyQuery query = new GetAuditoriumsForFacultyQuery()
            {
                FacultyId = id
            };
            AuditoriumsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новую запись об аудитории
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /auditoriums
        /// {
        ///     "auditoriumName": "string",
        ///     "positionInfo": "string",
        ///     "floorId": 0,
        ///     "teacherId": 0
        /// }
        /// </remarks>
        /// <param name="createAuditoriumDto">Объект CreateAuditoriumDto</param>
        /// <returns>Возвращает объект AuditoriumViewModel</returns>
        /// <response code="201">Запись об аудитории успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuditoriumViewModel>> Create([FromBody] CreateAuditoriumDto createAuditoriumDto)
        {
            CreateAuditoriumCommand command = _mapper.Map<CreateAuditoriumCommand>(createAuditoriumDto);
            AuditoriumViewModel auditorium = await Mediator.Send(command);
            return Created(nameof(AuditoriumsController), auditorium);
        }

        /// <summary>
        /// Редактирует информацию об аудитории
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /auditoriums
        /// {
        ///     "id": 1,
        ///     "auditoriumName": "string",
        ///     "positionInfo": "string",
        ///     "floorId": 0,
        ///     "teacherId": 0
        /// }        
        /// </remarks>
        /// <param name="updateAuditoriumDto">Объект UpdateAuditoriumDto</param>
        /// <returns>Возвращает объект AuditoriumViewModel</returns>
        /// <response code="200">Информация об аудитории успешно обновлена</response>
        /// <response code="404">Аудитория не найдена</response>
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
        public async Task<ActionResult<AuditoriumViewModel>> Update([FromBody] UpdateAuditoriumDto updateAuditoriumDto)
        {
            UpdateAuditoriumCommand command = _mapper.Map<UpdateAuditoriumCommand>(updateAuditoriumDto);
            AuditoriumViewModel auditorium = await Mediator.Send(command);
            return Ok(auditorium);
        }

        /// <summary>
        /// Удаляет информацию об аудитории по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /auditorium/1
        /// </remarks>
        /// <param name="id">id аудитории (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Информация об аудитории успешно удалена</response>
        /// <response code="404">Аудитория не найдена</response>
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
            DeleteAuditoriumCommand command = new DeleteAuditoriumCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
