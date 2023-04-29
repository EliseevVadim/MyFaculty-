using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Groups.Commands.CreateGroup;
using MyFaculty.Application.Features.Groups.Commands.DeleteGroup;
using MyFaculty.Application.Features.Groups.Commands.UpdateGroup;
using MyFaculty.Application.Features.Groups.Queries.GetGroupInfo;
using MyFaculty.Application.Features.Groups.Queries.GetGroups;
using MyFaculty.Application.Features.Groups.Queries.GetGroupsForCourse;
using MyFaculty.Application.Features.Groups.Queries.GetGroupsForFaculty;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class GroupsController : BaseController
    {
        private readonly IMapper _mapper;

        public GroupsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список групп
        /// </summary>
        /// <remarks>
        /// Пример запроса:  
        /// GET /groups
        /// </remarks>
        /// <returns>Возвращает объект GroupsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GroupsListViewModel>> GetAll()
        {
            GetGroupsQuery query = new GetGroupsQuery();
            GroupsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информацию о группе по id
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /groups/1
        /// </remarks>
        /// <param name="id">id группы (integer)</param>
        /// <returns>Возвращает объект GroupViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Группа не найдена</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GroupViewModel>> Get(int id)
        {
            GetGroupInfoQuery query = new GetGroupInfoQuery()
            {
                Id = id
            };
            GroupViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список групп, принадлежащих указанному факультету
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /groups/faculty/1
        /// </remarks>
        /// <param name="id">id факультета (integer)</param>
        /// <returns>Возвращает объект GroupsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("faculty/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GroupsListViewModel>> GetByFacultyId(int id)
        {
            GetGroupsForFacultyQuery query = new GetGroupsForFacultyQuery()
            {
                FacultyId = id
            };
            GroupsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список групп, принадлежащих указанному курсу
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /groups/course/1
        /// </remarks>
        /// <param name="id">id курса (integer)</param>
        /// <returns>Возвращает объект CoursesListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("course/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GroupsListViewModel>> GetByCourseId(int id)
        {
            GetGroupsForCourseQuery query = new GetGroupsForCourseQuery()
            {
                CourseId = id
            };
            GroupsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новую запись о группе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /groups
        /// {
        ///     "groupName": "string",
        ///     "courseId": 1
        /// }
        /// </remarks>
        /// <param name="createGroupDto">Объект CreateGroupDto</param>
        /// <returns>Возвращает объект GroupViewModel</returns>
        /// <response code="201">Запись о группе успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GroupViewModel>> Create([FromBody] CreateGroupDto createGroupDto)
        {
            CreateGroupCommand command = _mapper.Map<CreateGroupCommand>(createGroupDto);
            GroupViewModel group = await Mediator.Send(command);
            return Created(nameof(GroupsController), group);
        }

        /// <summary>
        /// Редактирует информацию о группе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /groups
        /// {
        ///     "id": 1,
        ///     "groupName": "string",
        ///     "courseId": 1
        /// }
        /// </remarks>
        /// <param name="updateGroupDto">Объект CreateGroupDto</param>
        /// <returns>Возвращает объект GroupViewModel</returns>
        /// <response code="200">Информация о группе успешно обновлена</response>
        /// <response code="404">Группа не найдена</response>
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
        public async Task<ActionResult<GroupViewModel>> Update([FromBody] UpdateGroupDto updateGroupDto)
        {
            UpdateGroupCommand command = _mapper.Map<UpdateGroupCommand>(updateGroupDto);
            GroupViewModel group = await Mediator.Send(command);
            return Ok(group);
        }

        /// <summary>
        /// Удаляет информацию о курсе по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /groups/1
        /// </remarks>
        /// <param name="id">id группы (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Информация о группе успешно удалена</response>
        /// <response code="404">Группа не найдена</response>
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
            DeleteGroupCommand command = new DeleteGroupCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
