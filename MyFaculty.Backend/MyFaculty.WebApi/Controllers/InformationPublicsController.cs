using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFaculty.Application.Features.InformationPublics.Commands.AddModeratorToInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.BanInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.BlockUserAtInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.CreateInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.DeleteAllInformationPublicContent;
using MyFaculty.Application.Features.InformationPublics.Commands.DeleteInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.DemoteInformationPublicModerator;
using MyFaculty.Application.Features.InformationPublics.Commands.JoinInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.LeaveInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.UnbanInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.UnblockUserAtInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.UpdateInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Queries.GetBannedInformationPublics;
using MyFaculty.Application.Features.InformationPublics.Queries.GetInformationPublicInfo;
using MyFaculty.Application.Features.InformationPublics.Queries.GetInformationPublics;
using MyFaculty.Application.Features.InformationPublics.Queries.GetInformationPublicsByName;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using MyFaculty.WebApi.Hubs;
using MyFaculty.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class InformationPublicsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly NotificationsHub _notificationsHub;
        private readonly string _appDomain;

        public InformationPublicsController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, NotificationsHub notificationsHub)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _notificationsHub = notificationsHub;
            _appDomain = configuration["AppDomain"];
        }

        /// <summary>
        /// Возвращает список информационных сообществ
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /informationpublics
        /// </remarks>
        /// <returns>Возвращает объект InformationPublicsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        [HttpGet]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<InformationPublicsListViewModel>> GetAll()
        {
            GetInformationPublicsQuery query = new GetInformationPublicsQuery();
            InformationPublicsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список заблокированных информационных сообществ
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /informationpublics/banned
        /// </remarks>
        /// <returns>Возвращает объект InformationPublicsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        [HttpGet("banned")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<InformationPublicsListViewModel>> GetBanned()
        {
            GetBannedInformationPublicsQuery query = new GetBannedInformationPublicsQuery();
            InformationPublicsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список информационных сообществ по поисковому запросу
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /informationpublics/search/name
        /// </remarks>
        /// <returns>Возвращает объект InformationPublicsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("search/{request}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InformationPublicsListViewModel>> GetByClubName(string request)
        {
            GetInformationPublicsByNameQuery query = new GetInformationPublicsByNameQuery()
            {
                SearchRequest = request
            };
            InformationPublicsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информационное сообщество по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /informationpublics/1
        /// </remarks>
        /// <param name="id">id информационного сообщества (integer)</param>
        /// <returns>Возвращает объект InformationPublicViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Информационное сообщество не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InformationPublicViewModel>> Get(int id)
        {
            GetInformationPublicInfoQuery query = new GetInformationPublicInfoQuery()
            {
                Id = id
            };
            InformationPublicViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новое информационное сообщество
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /informationpublics
        /// {
        ///     "publicName": "string",
        ///     "description": "string",
        ///     "image": "file",
        ///     "ownerId": 1,
        ///     "issuerId": 1
        /// }
        /// </remarks>
        /// <param name="createInformationPublicDto">Объект CreateInformationPublicDto</param>
        /// <returns>Возвращает объект InformationPublicViewModel</returns>
        /// <response code="201">Информационное сообщество успешно создано</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InformationPublicViewModel>> Create([FromForm] CreateInformationPublicDto createInformationPublicDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != createInformationPublicDto.IssuerId)
                return Forbid();
            string photoPath = string.Empty;
            string savePath = string.Empty;
            if (createInformationPublicDto.Image != null)
            {
                photoPath = Guid.NewGuid().ToString() + "_" + createInformationPublicDto.Image.FileName;
                savePath = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads/images/info-publics", photoPath);
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    createInformationPublicDto.Image.CopyTo(stream);
                }
            }
            CreateInformationPublicCommand command = _mapper.Map<CreateInformationPublicCommand>(createInformationPublicDto);
            command.ImagePath = String.IsNullOrEmpty(photoPath) ? string.Empty : _appDomain + "uploads/images/info-publics/" + photoPath;
            InformationPublicViewModel infoPublic = await Mediator.Send(command);
            return Created(nameof(InformationPublicsController), infoPublic);
        }

        /// <summary>
        /// Позволяет присоединиться к информационному сообществу
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /informationpublics/join
        ///     "userId": 1,
        ///     "publicId": 1
        /// }
        /// </remarks>
        /// <param name="joinInformationPublicDto">Объект JoinInformationPublicDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Пользователь успешно вступил в сообщество</response>
        /// <response code="404">Информационное сообщество не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("join")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Join([FromBody] JoinInformationPublicDto joinInformationPublicDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != joinInformationPublicDto.UserId)
                return Forbid();
            JoinInformationPublicCommand command = _mapper.Map<JoinInformationPublicCommand>(joinInformationPublicDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Позволяет покинуть информационное сообщество
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /informationpublics/leave
        ///     "userId": 1,
        ///     "publicId": 1
        /// }
        /// </remarks>
        /// <param name="leaveInformationPublicDto">Объект LeaveInformationPublicDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Пользователь успешно покинул сообщество</response>
        /// <response code="404">Информационное сообщество или пользователь не найдены</response>
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
        public async Task<IActionResult> Leave([FromBody] LeaveInformationPublicDto leaveInformationPublicDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != leaveInformationPublicDto.UserId)
                return Forbid();
            LeaveInformationPublicCommand command = _mapper.Map<LeaveInformationPublicCommand>(leaveInformationPublicDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Добавляет модератора в информационное сообщество
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /informationpublics/add-moderator
        ///     "issuerId": 1,
        ///     "moderatorId": 2,
        ///     "informationPublicId": 1
        /// }
        /// </remarks>
        /// <param name="addModeratorToInformationPublicDto">Объект AddModeratorToInformationPublicDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Модератор успешно добавлен в информационное сообщество</response>
        /// <response code="404">Информационное сообщество или пользователь не найдены</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("add-moderator")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddModerator([FromBody] AddModeratorToInformationPublicDto addModeratorToInformationPublicDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != addModeratorToInformationPublicDto.IssuerId)
                return Forbid();
            AddModeratorToInformationPublicCommand command = _mapper.Map<AddModeratorToInformationPublicCommand>(addModeratorToInformationPublicDto);
            int newModeratorId = await Mediator.Send(command);
            await _notificationsHub.MakeUserLoadNotificationsAsync(newModeratorId);
            return NoContent();
        }

        /// <summary>
        /// Снимает пользователя с должности модератора в информационном сообществе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /informationpublics/demote-moderator
        ///     "issuerId": 1,
        ///     "moderatorId": 2,
        ///     "informationPublicId": 1
        /// }
        /// </remarks>
        /// <param name="demoteInformationPublicModeratorDto">Объект DemoteInformationPublicModeratorDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Пользователь успешно снят с должности модератора в информационном сообществе</response>
        /// <response code="404">Информационное сообщество или пользователь не найдены</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("demote-moderator")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DemoteModerator([FromBody] DemoteInformationPublicModeratorDto demoteInformationPublicModeratorDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != demoteInformationPublicModeratorDto.IssuerId)
                return Forbid();
            DemoteInformationPublicModeratorCommand command = _mapper.Map<DemoteInformationPublicModeratorCommand>(demoteInformationPublicModeratorDto);
            int demotedModeratorId = await Mediator.Send(command);
            await _notificationsHub.MakeUserLoadNotificationsAsync(demotedModeratorId);
            return NoContent();
        }

        /// <summary>
        /// Блокирует пользователя в информационном сообществе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /informationpublics/block-user
        ///     "userId": 1,
        ///     "publicId": 1,
        ///     "issuerId": 2
        /// }
        /// </remarks>
        /// <param name="blockUserAtInformationPublicDto">Объект BlockUserAtInformationPublicDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Пользователь успешно заблокирован в информационном сообществе</response>
        /// <response code="404">Информационное сообщество или пользователь не найдены</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("block-user")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BlockUser([FromBody] BlockUserAtInformationPublicDto blockUserAtInformationPublicDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != blockUserAtInformationPublicDto.IssuerId)
                return Forbid();
            BlockUserAtInformationPublicCommand command = _mapper.Map<BlockUserAtInformationPublicCommand>(blockUserAtInformationPublicDto);
            int blockedUserId = await Mediator.Send(command);
            await _notificationsHub.MakeUserLoadNotificationsAsync(blockedUserId);
            return NoContent();
        }

        /// <summary>
        /// Разблокирывает пользователя в информационном сообществе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /informationpublics/unblock-user
        ///     "userId": 1,
        ///     "publicId": 1,
        ///     "issuerId": 2
        /// }
        /// </remarks>
        /// <param name="unblockUserAtInformationPublicDto">Объект UnblockUserAtInformationPublicDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Пользователь успешно разблокирован в информационном сообществе</response>
        /// <response code="404">Информационное сообщество или пользователь не найдены</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("unblock-user")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UnblockUser([FromBody] UnblockUserAtInformationPublicDto unblockUserAtInformationPublicDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != unblockUserAtInformationPublicDto.IssuerId)
                return Forbid();
            UnblockUserAtInformationPublicCommand command = _mapper.Map<UnblockUserAtInformationPublicCommand>(unblockUserAtInformationPublicDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Редактирует информацию об информационном сообществе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /informationpublics
        /// {
        ///     "id": 1,
        ///     "publicName": "string",
        ///     "description": "string",
        ///     "image": "file",
        ///     "ownerId": 1,
        ///     "issuerId": 1
        /// }
        /// </remarks>
        /// <param name="updateInformationPublicDto">Объект UpdateInformationPublicDto</param>
        /// <returns>Возвращает объект InformationPublicViewModel</returns>
        /// <response code="200">Информация об информационном сообществе успешно обновлена</response>
        /// <response code="404">Информационное сообщество не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response>  
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPut]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InformationPublicViewModel>> Update([FromForm] UpdateInformationPublicDto updateInformationPublicDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != updateInformationPublicDto.IssuerId)
                return Forbid();
            string photoPath = string.Empty;
            string savePath = string.Empty;
            if (updateInformationPublicDto.Image != null)
            {
                photoPath = Guid.NewGuid().ToString() + "_" + updateInformationPublicDto.Image.FileName;
                savePath = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads/images/info-publics", photoPath);
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    updateInformationPublicDto.Image.CopyTo(stream);
                }
            }
            UpdateInformationPublicCommand command = _mapper.Map<UpdateInformationPublicCommand>(updateInformationPublicDto);
            command.ImagePath = String.IsNullOrEmpty(photoPath) ? string.Empty : _appDomain + "uploads/images/info-publics/" + photoPath;
            InformationPublicViewModel infoPublic = await Mediator.Send(command);
            return Ok(infoPublic);
        }

        /// <summary>
        /// Удаляет информационное сообщество по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /informationpublics/1
        /// </remarks>
        /// <param name="id">id информационного сообщества (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Информационное сообщество успешно удалено</response>
        /// <response code="404">Информационное сообщество не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            DeleteInformationPublicCommand command = new DeleteInformationPublicCommand()
            {
                Id = id,
                IssuerId = requesterId
            };
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Удаляет все содержимое информационного сообщества (записи, комментарии, их вложения) по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /informationpublics/content/1
        /// </remarks>
        /// <param name="id">id информационного сообщества (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Cодержимое информационного сообщества успешно удалено</response>
        /// <response code="404">Информационное сообщество не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpDelete("content/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePublicContent(int id)
        {
            DeleteAllInformationPublicContentCommand command = new DeleteAllInformationPublicContentCommand()
            {
                PublicId = id
            };
            List<string> attachmentsToDelete = await Mediator.Send(command);
            foreach (string attachmentsData in attachmentsToDelete)
            {
                if (!string.IsNullOrEmpty(attachmentsData))
                {
                    List<Attachment> filesToDelete = JsonConvert.DeserializeObject<List<Attachment>>(attachmentsData);
                    DeleteAttachments(filesToDelete);
                }
            }
            return NoContent();
        }

        /// <summary>
        /// Блокирует информационное сообщество
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /informationpublics/ban
        /// {
        ///     "bannedPublicId": 1,
        ///     "administratorId": 1,
        ///     "reason": "spam"
        /// }
        /// </remarks>
        /// <param name="banInformationPublicDto">Объект BanInformationPublicDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="200">Информационное сообщество успешно заблокировано</response>
        /// <response code="404">Информационное сообщество не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("ban")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Ban([FromBody] BanInformationPublicDto banInformationPublicDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            BanInformationPublicCommand command = _mapper.Map<BanInformationPublicCommand>(banInformationPublicDto);
            command.AdministratorId = requesterId;
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Разблокирывает информационное сообщество
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /informationpublics/unban
        /// {
        ///     "unbannedPublicId": 1,
        ///     "administratorId": 1,
        ///     "reason": "amnestied"
        /// }
        /// </remarks>
        /// <param name="unbanInformationPublicDto">Объект UnbanInformationPublicDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="200">Информационное сообщество успешно заблокировано</response>
        /// <response code="404">Информационное сообщество не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("unban")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Unban([FromBody] UnbanInformationPublicDto unbanInformationPublicDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UnbanInformationPublicCommand command = _mapper.Map<UnbanInformationPublicCommand>(unbanInformationPublicDto);
            command.AdministratorId = requesterId;
            await Mediator.Send(command);
            return Ok();
        }

        private void DeleteAttachments(List<Attachment> attachmentsToDelete)
        {
            foreach (Attachment attachment in attachmentsToDelete)
            {
                string path = attachment.Path
                    .Replace(_appDomain, $"{_webHostEnvironment.ContentRootPath}/");
                System.IO.File.Delete(path);
            }
        }
    }
}
