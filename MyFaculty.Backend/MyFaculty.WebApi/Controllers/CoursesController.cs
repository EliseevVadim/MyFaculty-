using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Courses.Commands.CreateCourse;
using MyFaculty.Application.Features.Courses.Commands.DeleteCourse;
using MyFaculty.Application.Features.Courses.Commands.UpdateCourse;
using MyFaculty.Application.Features.Courses.Queries.GetCourseInfo;
using MyFaculty.Application.Features.Courses.Queries.GetCourses;
using MyFaculty.Application.Features.Courses.Queries.GetCoursesForFaculty;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CoursesController : BaseController
    {
        private IMapper _mapper;

        public CoursesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список курсов
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /cousres
        /// </remarks>
        /// <returns>Возвращает объект CoursesListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CoursesListViewModel>> GetAll()
        {
            GetCoursesQuery query = new GetCoursesQuery();
            CoursesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информацию о курсе по id
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /cousres/1
        /// </remarks>
        /// <param name="id">id курса (integer)</param>
        /// <returns>Возвращает объект CourseViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Курс не найден</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CourseViewModel>> Get(int id)
        {
            GetCourseInfoQuery query = new GetCourseInfoQuery()
            {
                Id = id
            };
            CourseViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список курсов, принадлежащих указанному факультету
        /// </summary>
        /// <remarks>
        /// Пример запроса:  
        /// GET /courses/faculty/1
        /// </remarks>
        /// <param name="id">id факультета (integer)</param>
        /// <returns>Возвращает объект CoursesListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("faculty/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CoursesListViewModel>> GetByFacultyId(int id)
        {
            GetCoursesForFacultyQuery query = new GetCoursesForFacultyQuery()
            {
                FacultyId = id
            };
            CoursesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новую запись о курсе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /courses
        /// {
        ///     "courseName": "string",
        ///     "courseNumber": 0,
        ///     "facultyId": 0
        /// }
        /// </remarks>
        /// <param name="createCourseDto">Объект CreateCourseDto</param>
        /// <returns>Возвращает объект CourseViewModel</returns>
        /// <response code="201">Запись о курсе успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CourseViewModel>> Create([FromBody] CreateCourseDto createCourseDto)
        {
            CreateCourseCommand command = _mapper.Map<CreateCourseCommand>(createCourseDto);
            CourseViewModel course = await Mediator.Send(command);
            return Created(nameof(CoursesController), course);
        }

        /// <summary>
        /// Редактирует информацию о курсе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /courses
        /// {
        ///     "id": 1,
        ///     "courseName": "string",
        ///     "courseNumber": 0,
        ///     "facultyId": 0
        /// }
        /// </remarks>
        /// <param name="updateCourseDto">Объект UpdateCourseDto</param>
        /// <returns>Возвращает объект CourseViewModel</returns>
        /// <response code="200">Информация о курсе успешно обновлена</response>
        /// <response code="404">Курс не найден</response>
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
        public async Task<ActionResult<CourseViewModel>> Update([FromBody] UpdateCourseDto updateCourseDto)
        {
            UpdateCourseCommand command = _mapper.Map<UpdateCourseCommand>(updateCourseDto);
            CourseViewModel course = await Mediator.Send(command);
            return Ok(course);
        }

        /// <summary>
        /// Удаляет информацию о курсе по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /courses/1
        /// </remarks>
        /// <param name="id">id курса (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Информация о курсе успешно удалена</response>
        /// <response code="404">Курс не найден</response>
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
            DeleteCourseCommand command = new DeleteCourseCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
