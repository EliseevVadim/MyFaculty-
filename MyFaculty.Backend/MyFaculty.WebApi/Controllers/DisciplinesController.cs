using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Disciplines.Commands.CreateDiscipline;
using MyFaculty.Application.Features.Disciplines.Commands.DeleteDiscipline;
using MyFaculty.Application.Features.Disciplines.Commands.UpdateDiscipline;
using MyFaculty.Application.Features.Disciplines.Queries.GetDisciplineInfo;
using MyFaculty.Application.Features.Disciplines.Queries.GetDisciplines;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DisciplinesController : BaseController
    {
        private IMapper _mapper;

        public DisciplinesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список дисциплин
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /disciplines
        /// </remarks>
        /// <returns>Возвращает объект DisciplinesListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DisciplinesListViewModel>> GetAll()
        {
            GetDisciplinesQuery query = new GetDisciplinesQuery();
            DisciplinesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информацию о дисциплине по id
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /disciplines/1
        /// </remarks>
        /// <param name="id">id дисциплины (integer)</param>
        /// <returns>Возвращает объект DisciplineViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Дисциплина не найдена</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DisciplineViewModel>> Get(int id)
        {
            GetDisciplineInfoQuery query = new GetDisciplineInfoQuery()
            {
                Id = id
            };
            DisciplineViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новую запись о дисциплине
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /disciplines
        /// {
        ///     "disciplineName": "string"
        /// }
        /// </remarks>
        /// <param name="createDisciplineDto">Объект CreateDisciplineDto</param>
        /// <returns>Возвращает объект DisciplineViewModel</returns>
        /// <response code="201">Запись о дисциплине успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DisciplineViewModel>> Create([FromBody] CreateDisciplineDto createDisciplineDto)
        {
            CreateDisciplineCommand command = _mapper.Map<CreateDisciplineCommand>(createDisciplineDto);
            DisciplineViewModel discipline = await Mediator.Send(command);
            return Created(nameof(DisciplinesController), discipline);
        }

        /// <summary>
        /// Редактирует информацию о дисциплине
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /disciplines
        /// {
        ///     "id": 1,
        ///     "disciplineName": "string"
        /// }
        /// </remarks>
        /// <param name="updateDisciplineDto">Объект UpdateDisciplineDto</param>
        /// <returns>Возвращает объект DisciplineViewModel</returns>
        /// <response code="200">Информация о дисциплине успешно обновлена</response>
        /// <response code="404">Дисциплина не найдена</response>
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
        public async Task<ActionResult<DisciplineViewModel>> Update([FromBody] UpdateDisciplineDto updateDisciplineDto)
        {
            UpdateDisciplineCommand command = _mapper.Map<UpdateDisciplineCommand>(updateDisciplineDto);
            DisciplineViewModel discipline = await Mediator.Send(command);
            return Ok(discipline);
        }

        /// <summary>
        /// Удаляет информацию о дисциплине по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /disciplines/1
        /// </remarks>
        /// <param name="id">id дисциплины (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Информация о дисциплине успешно удалена</response>
        /// <response code="404">Дисциплина не найдена</response>
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
            DeleteDisciplineCommand command = new DeleteDisciplineCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
