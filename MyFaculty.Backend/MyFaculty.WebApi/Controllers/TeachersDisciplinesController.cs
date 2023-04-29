using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.TeachersDisciplines.Commands.CreateTeacherDiscipline;
using MyFaculty.Application.Features.TeachersDisciplines.Commands.DeleteTeacherDiscipline;
using MyFaculty.Application.Features.TeachersDisciplines.Commands.UpdateTeacherDiscipline;
using MyFaculty.Application.Features.TeachersDisciplines.Queries.GetTeacherDisciplineInfo;
using MyFaculty.Application.Features.TeachersDisciplines.Queries.GetTeachersDisciplines;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TeachersDisciplinesController : BaseController
    {
        private IMapper _mapper;

        public TeachersDisciplinesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список связок преподаватель-дисциплина
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /teachersdisciplines
        /// </remarks>
        /// <returns>Возвращает объект TeachersDisciplinesListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TeachersDisciplinesListViewModel>> GetAll()
        {
            GetTeachersDisciplinesQuery query = new GetTeachersDisciplinesQuery();
            TeachersDisciplinesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает связку преподаватель-дисциплина по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /teachersdisciplines/1
        /// </remarks>
        /// <param name="id">id связки преподаватель-дисциплина (integer)</param>
        /// <returns>Возвращает объект TeacherDisciplineViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Связка преподаватель-дисциплина не найдена</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeacherDisciplineViewModel>> Get(int id)
        {
            GetTeacherDisciplineInfoQuery query = new GetTeacherDisciplineInfoQuery()
            {
                Id = id
            };
            TeacherDisciplineViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Добавляет дисциплину преподавателю
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /teachersdisciplines
        /// {
        ///     "teacherId": 1,
        ///     "disciplineId": 1
        /// }
        /// </remarks>
        /// <param name="createTeacherDisciplineDto">Объект CreateTeacherDisciplineDto</param>
        /// <returns>Возвращает объект TeacherDisciplineViewModel</returns>
        /// <response code="201">Дисциплина успешно добавлена преподавателю</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TeacherDisciplineViewModel>> Create([FromBody] CreateTeacherDisciplineDto createTeacherDisciplineDto)
        {
            CreateTeacherDisciplineCommand command = _mapper.Map<CreateTeacherDisciplineCommand>(createTeacherDisciplineDto);
            TeacherDisciplineViewModel teacherDiscipline = await Mediator.Send(command);
            return Created(nameof(TeachersDisciplinesController), teacherDiscipline);
        }

        /// <summary>
        /// Редактирует связку преподаватель-дисциплина
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /teachersdisciplines
        /// {
        ///     "id": 1,
        ///     "teacherId": 1,
        ///     "disciplineId": 1
        /// }
        /// </remarks>
        /// <param name="updateTeacherDisciplineDto">Объект UpdateTeacherDisciplineDto</param>
        /// <returns>Возвращает объект TeacherDisciplineViewModel</returns>
        /// <response code="200">Связка преподаватель-дисциплина успешно обновлена</response>
        /// <response code="404">Связка преподаватель-дисциплина не найдена</response>
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
        public async Task<ActionResult<TeacherDisciplineViewModel>> Update([FromBody] UpdateTeacherDisciplineDto updateTeacherDisciplineDto)
        {
            UpdateTeacherDisciplineCommand command = _mapper.Map<UpdateTeacherDisciplineCommand>(updateTeacherDisciplineDto);
            TeacherDisciplineViewModel teacherDiscipline = await Mediator.Send(command);
            return Ok(teacherDiscipline);
        }

        /// <summary>
        /// Удаляет дисциплину у преподавателя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /teachersdisciplines/1
        /// </remarks>
        /// <param name="id">id связки дисциплина-преподаватель (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Дисциплина у преподавателя успешно удалена</response>
        /// <response code="404">Связка преподаватель-дисциплина не найдена</response>
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
            DeleteTeacherDisciplineCommand command = new DeleteTeacherDisciplineCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
