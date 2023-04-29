using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFaculty.Application.Features.StudyClubs.Commands.AddModeratorToStudyClub;
using MyFaculty.Application.Features.StudyClubs.Commands.AddUsersToStudyClubByCourse;
using MyFaculty.Application.Features.StudyClubs.Commands.AddUsersToStudyClubByGroup;
using MyFaculty.Application.Features.StudyClubs.Commands.CreateStudyClub;
using MyFaculty.Application.Features.StudyClubs.Commands.DeleteStudyClub;
using MyFaculty.Application.Features.StudyClubs.Commands.DemoteStudyClubModerator;
using MyFaculty.Application.Features.StudyClubs.Commands.JoinStudyClub;
using MyFaculty.Application.Features.StudyClubs.Commands.LeaveStudyClub;
using MyFaculty.Application.Features.StudyClubs.Commands.RemoveUserFromStudyClub;
using MyFaculty.Application.Features.StudyClubs.Commands.RemoveUsersFromStudyClubByCourse;
using MyFaculty.Application.Features.StudyClubs.Commands.RemoveUsersFromStudyClubByGroup;
using MyFaculty.Application.Features.StudyClubs.Commands.UpdateStudyClub;
using MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubInfo;
using MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubs;
using MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubsByName;
using MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubsForSpecificUser;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using MyFaculty.WebApi.Hubs;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class StudyClubsController : BaseController
    {
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;
        private NotificationsHub _notificationsHub;
        private string _appDomain;

        public StudyClubsController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, NotificationsHub notificationsHub)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _notificationsHub = notificationsHub;
            _appDomain = configuration["AppDomain"];
        }

        /// <summary>
        /// Возвращает список сообществ курсов
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /studyclubs
        /// </remarks>
        /// <returns>Возвращает объект StudyClubsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        [HttpGet]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StudyClubsListViewModel>> GetAll()
        {
            GetStudyClubsQuery query = new GetStudyClubsQuery();
            StudyClubsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список сообществ курсов, в которых состоит текущий пользователь
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /studyclubs/user/1
        /// </remarks>
        /// <returns>Возвращает объект StudyClubsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("user/{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudyClubsListViewModel>> GetForSpecificUser(int id)
        {
            GetStudyClubsForSpecificUserQuery query = new GetStudyClubsForSpecificUserQuery()
            {
                UserId = id
            };
            StudyClubsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список сообществ курсов по поисковому запросу
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /studyclubs/search/name
        /// </remarks>
        /// <returns>Возвращает объект StudyClubsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("search/{request}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudyClubsListViewModel>> GetByClubName(string request)
        {
            GetStudyClubsByNameQuery query = new GetStudyClubsByNameQuery()
            {
                SearchRequest = request
            };
            StudyClubsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает сообщество курса по id
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /studyclubs/1
        /// </remarks>
        /// <param name="id">id сообщества курса (integer)</param>
        /// <returns>Возвращает объект StudyClubViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Сообщество курса не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudyClubViewModel>> Get(int id)
        {
            GetStudyClubInfoQuery query = new GetStudyClubInfoQuery()
            {
                Id = id
            };
            StudyClubViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новое сообщество курса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /studyclubs
        /// {
        ///     "studyClubName": "string",
        ///     "description": "string",
        ///     "image": "file",
        ///     "ownerId": 1
        /// }
        /// </remarks>
        /// <param name="createStudyClubDto">Объект CreateStudyClubDto</param>
        /// <returns>Возвращает объект StudyClubViewModel</returns>
        /// <response code="201">Информационное сообщество успешно создано</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudyClubViewModel>> Create([FromForm] CreateStudyClubDto createStudyClubDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != createStudyClubDto.OwnerId)
                return Forbid();
            string photoPath = string.Empty;
            string savePath = string.Empty;
            if (createStudyClubDto.Image != null)
            {
                photoPath = Guid.NewGuid().ToString() + "_" + createStudyClubDto.Image.FileName;
                savePath = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads/images/study-clubs", photoPath);
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    createStudyClubDto.Image.CopyTo(stream);
                }
            }
            CreateStudyClubCommand command = _mapper.Map<CreateStudyClubCommand>(createStudyClubDto);
            command.ImagePath = String.IsNullOrEmpty(photoPath) ? string.Empty : _appDomain + "uploads/images/study-clubs/" + photoPath;
            StudyClubViewModel studyClub = await Mediator.Send(command);
            return Created(nameof(StudyClubsController), studyClub);
        }

        /// <summary>
        /// Позволяет присоединиться к сообществу курса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /studyclubs/join
        ///     "userId": 1,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="joinStudyClubDto">Объект JoinStudyClubDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Пользователь успешно вступил в сообщество</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("join")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Join([FromBody] JoinStudyClubDto joinStudyClubDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != joinStudyClubDto.UserId)
                return Forbid();
            JoinStudyClubCommand command = _mapper.Map<JoinStudyClubCommand>(joinStudyClubDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Позволяет покинуть сообщество курса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /studyclubs/leave
        ///     "userId": 1,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="leaveStudyClubDto">Объект LeaveStudyClubDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Пользователь успешно покинул сообщество</response>
        /// <response code="404">Сообщество курса или пользователь не найдены</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("leave")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Leave([FromBody] LeaveStudyClubDto leaveStudyClubDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != leaveStudyClubDto.UserId)
                return Forbid();
            LeaveStudyClubCommand command = _mapper.Map<LeaveStudyClubCommand>(leaveStudyClubDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Добавляет модератора в сообщество курса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /studyclubs/add-moderator
        ///     "issuerId": 1,
        ///     "moderatorId": 2,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="addModeratorToStudyClubDto">Объект AddModeratorToStudyClubDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Модератор успешно добавлен в сообщество курса</response>
        /// <response code="404">Сообщество курса или пользователь не найдены</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("add-moderator")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddModerator([FromBody] AddModeratorToStudyClubDto addModeratorToStudyClubDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != addModeratorToStudyClubDto.IssuerId)
                return Forbid();
            AddModeratorToStudyClubCommand command = _mapper.Map<AddModeratorToStudyClubCommand>(addModeratorToStudyClubDto);
            int newModeratorId = await Mediator.Send(command);
            await _notificationsHub.MakeUserLoadNotificationsAsync(newModeratorId);
            return NoContent();
        }

        /// <summary>
        /// Снимает пользователя с должности модератора в сообществе курса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /studyclubs/demote-moderator
        ///     "issuerId": 1,
        ///     "moderatorId": 2,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="demoteStudyClubModeratorDto">Объект DemoteStudyClubModeratorDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Пользователь успешно снят с должности модератора в сообществе курса</response>
        /// <response code="404">Сообщество курса или пользователь не найдены</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("demote-moderator")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DemoteModerator([FromBody] DemoteStudyClubModeratorDto demoteStudyClubModeratorDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != demoteStudyClubModeratorDto.IssuerId)
                return Forbid();
            DemoteStudyClubModeratorCommand command = _mapper.Map<DemoteStudyClubModeratorCommand>(demoteStudyClubModeratorDto);
            int demotedModeratorId = await Mediator.Send(command);
            await _notificationsHub.MakeUserLoadNotificationsAsync(demotedModeratorId);
            return NoContent();
        }

        /// <summary>
        /// Удаляет пользователя из сообщества курса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /studyclubs/remove-user
        ///     "issuerId": 1,
        ///     "removingUserId": 2,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="removeUserFromStudyClubDto">Объект RemoveUserFromStudyClubDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Пользователь успешно удален из сообщества курса</response>
        /// <response code="404">Сообщество курса или пользователь не найдены</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("remove-user")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveUser([FromBody] RemoveUserFromStudyClubDto removeUserFromStudyClubDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != removeUserFromStudyClubDto.IssuerId)
                return Forbid();
            RemoveUserFromStudyClubCommand command = _mapper.Map<RemoveUserFromStudyClubCommand>(removeUserFromStudyClubDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Добавляет пользователей из заданной группы в сообщество курса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /studyclubs/add-users-from-group
        ///     "issuerId": 1,
        ///     "groupId": 2,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="addUsersToStudyClubByGroupDto">Объект AddUsersToStudyClubByGroupDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Пользователи успешно добавлены</response>
        /// <response code="404">Сообщество курса не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("add-users-from-group")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUsersFromGroup([FromBody] AddUsersToStudyClubByGroupDto addUsersToStudyClubByGroupDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != addUsersToStudyClubByGroupDto.IssuerId)
                return Forbid();
            AddUsersToStudyClubByGroupCommand command = _mapper.Map<AddUsersToStudyClubByGroupCommand>(addUsersToStudyClubByGroupDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Добавляет пользователей из заданного курса в сообщество курса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /studyclubs/add-users-from-course
        ///     "issuerId": 1,
        ///     "courseId": 2,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="addUsersToStudyClubByCourseDto">Объект AddUsersToStudyClubByCourseDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Пользователи успешно добавлены</response>
        /// <response code="404">Сообщество курса не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("add-users-from-course")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUsersFromCourse([FromBody] AddUsersToStudyClubByCourseDto addUsersToStudyClubByCourseDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != addUsersToStudyClubByCourseDto.IssuerId)
                return Forbid();
            AddUsersToStudyClubByCourseCommand command = _mapper.Map<AddUsersToStudyClubByCourseCommand>(addUsersToStudyClubByCourseDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Исключает пользователей из заданной группы из сообщества курса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /studyclubs/remove-users-from-group
        ///     "issuerId": 1,
        ///     "groupId": 2,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="removeUsersFromStudyClubByGroupDto">Объект RemoveUsersFromStudyClubByGroupDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Пользователи успешно исключены</response>
        /// <response code="404">Сообщество курса не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("remove-users-from-group")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveUsersByGroup([FromBody] RemoveUsersFromStudyClubByGroupDto removeUsersFromStudyClubByGroupDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != removeUsersFromStudyClubByGroupDto.IssuerId)
                return Forbid();
            RemoveUsersFromStudyClubByGroupCommand command = _mapper.Map<RemoveUsersFromStudyClubByGroupCommand>(removeUsersFromStudyClubByGroupDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Исключает пользователей из заданного курса из сообщества курса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /studyclubs/remove-users-from-course
        ///     "issuerId": 1,
        ///     "courseId": 2,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="removeUsersFromStudyClubByCourseDto">Объект RemoveUsersFromStudyClubByCourseDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Пользователи успешно исключены</response>
        /// <response code="404">Сообщество курса не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("remove-users-from-course")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveUsersByCourse([FromBody] RemoveUsersFromStudyClubByCourseDto removeUsersFromStudyClubByCourseDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != removeUsersFromStudyClubByCourseDto.IssuerId)
                return Forbid();
            RemoveUsersFromStudyClubByCourseCommand command = _mapper.Map<RemoveUsersFromStudyClubByCourseCommand>(removeUsersFromStudyClubByCourseDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Редактирует информацию о сообществе курса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /studyclubs
        /// {
        ///     "id": 1,
        ///     "studyClubName": "string",
        ///     "description": "string",
        ///     "image": "file",
        ///     "ownerId": 1
        /// }
        /// </remarks>
        /// <param name="updateStudyClubDto">Объект UpdateStudyClubDto</param>
        /// <returns>Возвращает объект StudyClubViewModel</returns>
        /// <response code="200">Информация о сообществе курса успешно обновлена</response>
        /// <response code="404">Сообщество курса не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response>  
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPut]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudyClubViewModel>> Update([FromForm] UpdateStudyClubDto updateStudyClubDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != updateStudyClubDto.IssuerId)
                return Forbid();
            string photoPath = string.Empty;
            string savePath = string.Empty;
            if (updateStudyClubDto.Image != null)
            {
                photoPath = Guid.NewGuid().ToString() + "_" + updateStudyClubDto.Image.FileName;
                savePath = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads/images/study-clubs", photoPath);
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    updateStudyClubDto.Image.CopyTo(stream);
                }
            }
            UpdateStudyClubCommand command = _mapper.Map<UpdateStudyClubCommand>(updateStudyClubDto);
            command.ImagePath = String.IsNullOrEmpty(photoPath) ? string.Empty : _appDomain + "uploads/images/study-clubs/" + photoPath;
            StudyClubViewModel studyClub = await Mediator.Send(command);
            return Ok(studyClub);
        }

        /// <summary>
        /// Удаляет сообщество курса по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /studyclubs/1
        /// </remarks>
        /// <param name="id">id сообщества курса (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Сообщество курса успешно удалено</response>
        /// <response code="404">Сообщество курса не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            DeleteStudyClubCommand command = new DeleteStudyClubCommand()
            {
                Id = id,
                IssuerId = requesterId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
