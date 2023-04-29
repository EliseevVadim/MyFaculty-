using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Faculties.Commands.CreateFaculty;
using MyFaculty.Application.Features.Faculties.Commands.DeleteFaculty;
using MyFaculty.Application.Features.Faculties.Commands.UpdateFaculty;
using MyFaculty.Application.Features.Faculties.Queries.GetFaculties;
using MyFaculty.Application.Features.Faculties.Queries.GetFacultyInfo;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FacultiesController : BaseController
    {
        private readonly IMapper _mapper;

        public FacultiesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список факультетов
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /faculties
        /// </remarks>
        /// <returns>Возвращает объект FacultiesListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<FacultiesListViewModel>> GetAll()
        {
            GetFacultiesQuery query = new GetFacultiesQuery();
            FacultiesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информацию о факультете по id
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /faculties/1
        /// </remarks>
        /// <param name="id">id факультета (integer)</param>
        /// <returns>Возвращает объект FacultyViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Факультет не найден</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FacultyViewModel>> Get(int id)
        {
            GetFacultyInfoQuery query = new GetFacultyInfoQuery()
            {
                Id = id
            };
            FacultyViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новую запись о факультете
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /faculties
        /// {
        ///     "facultyName": "string",
        ///     "officialWebsite": "string"
        /// }
        /// </remarks>
        /// <param name="createFacultyDto">Объект CreateFacultyDto</param>
        /// <returns>Возвращает объект FacultyViewModel</returns>
        /// <response code="201">Запись о факультете успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FacultyViewModel>> Create([FromBody] CreateFacultyDto createFacultyDto)
        {
            CreateFacultyCommand command = _mapper.Map<CreateFacultyCommand>(createFacultyDto);
            FacultyViewModel faculty = await Mediator.Send(command);
            return Created(nameof(FacultiesController), faculty);
        }

        /// <summary>
        /// Редактирует информацию о факультете
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /faculties
        /// {
        ///     "id": 1,
        ///     "facultyName": "string",
        ///     "officialWebsite": "string"
        /// }
        /// </remarks>
        /// <param name="updateFacultyDto">Объект UpdateFacultyDto</param>
        /// <returns>Возвращает объект FacultyViewModel</returns>
        /// <response code="200">Информация о факультете успешно обновлена</response>
        /// <response code="404">Факультет не найден</response>
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
        public async Task<ActionResult<FacultyViewModel>> Update([FromBody] UpdateFacultyDto updateFacultyDto)
        {
            UpdateFacultyCommand command = _mapper.Map<UpdateFacultyCommand>(updateFacultyDto);
            FacultyViewModel faculty = await Mediator.Send(command);
            return Ok(faculty);
        }

        /// <summary>
        /// Удаляет информацию о факультете по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /faculties/1
        /// </remarks>
        /// <param name="id">id факультета (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Информация о факультете успешно удалена</response>
        /// <response code="404">Факультет не найден</response>
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
            DeleteFacultyCommand command = new DeleteFacultyCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
