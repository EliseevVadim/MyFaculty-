using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFaculty.Application.Features.ClubTasks.Commands.CreateClubTask;
using MyFaculty.Application.Features.ClubTasks.Commands.DeleteClubTask;
using MyFaculty.Application.Features.ClubTasks.Commands.UpdateClubTask;
using MyFaculty.Application.Features.ClubTasks.Queries.GetClubTaskInfo;
using MyFaculty.Application.Features.ClubTasks.Queries.GetClubTasksForStudyClub;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using MyFaculty.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ClubTasksController : BaseController
    {
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;
        private string _appDomain;

        public ClubTasksController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _appDomain = configuration["AppDomain"];
        }

        /// <summary>
        /// Возвращает список заданий, принадлежащих указанному сообществу курса
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /clubtasks/study-club/1
        /// </remarks>
        /// <param name="id">id сообщества курса (integer)</param>
        /// <returns>Возвращает объект ClubTasksListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("study-club/{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClubTasksListViewModel>> GetByStudyClubId(int id)
        {
            GetClubTasksForStudyClubQuery query = new GetClubTasksForStudyClubQuery()
            {
                StudyClubId = id
            };
            ClubTasksListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информацию о задании по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:  
        /// GET /clubtasks/1
        /// </remarks>
        /// <param name="id">id задания (integer)</param>
        /// <returns>Возвращает объект ClubTaskViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Задание не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClubTaskViewModel>> Get(int id)
        {
            GetClubTaskInfoQuery query = new GetClubTaskInfoQuery()
            {
                Id = id
            };
            ClubTaskViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новое задание
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /clubtasks
        /// {
        ///     "textContent": "string",
        ///     "postAttachments": "list of files",
        ///     "studyClubId": 1,
        ///     "authorId": 1,
        ///     "deadLine": "future date time"
        /// }
        /// </remarks>
        /// <param name="createClubTaskDto">Объект CreateClubTaskDto</param>
        /// <returns>Возвращает объект ClubTaskViewModel</returns>
        /// <response code="201">Задание успешно создано</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClubTaskViewModel>> Create([FromForm] CreateClubTaskDto createClubTaskDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != createClubTaskDto.AuthorId)
                return Forbid();
            List<Attachment> postAttachments = new List<Attachment>();
            Guid postAttachmentsUid = Guid.NewGuid();
            List<Attachment> newFiles = ProcessNewFiles(createClubTaskDto.PostAttachments, postAttachmentsUid);
            if (newFiles != null)
                postAttachments.AddRange(newFiles);
            CreateClubTaskCommand command = _mapper.Map<CreateClubTaskCommand>(createClubTaskDto);
            command.Attachments = postAttachments.Count > 0 ? JsonConvert.SerializeObject(postAttachments) : null;
            command.PostAttachmentsUid = postAttachmentsUid;
            ClubTaskViewModel task = await Mediator.Send(command);
            return Created(nameof(ClubTasksController), task);
        }

        /// <summary>
        /// Редактирует задание
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /clubtasks
        /// {
        ///     "id": 1,
        ///     "textContent": "string",
        ///     "oldAttachments": "json",
        ///     "actualAttachments": "json",
        ///     "newFiles": "list of files",
        ///     "postAttachmentsUid": "guid"
        ///     "issuerId": 1,
        ///     "deadLine": "future date time"
        /// }
        /// </remarks>
        /// <param name="updateClubTaskDto">Объект UpdateClubTaskDto</param>
        /// <returns>Возвращает объект ClubTaskViewModel</returns>
        /// <response code="200">Задание успешно обновлено</response>
        /// <response code="404">Задание не найдено</response>
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
        public async Task<ActionResult<ClubTaskViewModel>> Update([FromForm] UpdateClubTaskDto updateClubTaskDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != updateClubTaskDto.IssuerId)
                return Forbid();
            string oldAttachments = updateClubTaskDto.OldAttachments;
            string actualAttachments = updateClubTaskDto.ActualAttachments;
            List<Attachment> postAttachments = actualAttachments == null ? new List<Attachment>() : JsonConvert.DeserializeObject<List<Attachment>>(actualAttachments);
            List<Attachment> newFiles = ProcessNewFiles(updateClubTaskDto.NewFiles, updateClubTaskDto.PostAttachmentsUid);
            if (newFiles != null)
                postAttachments.AddRange(newFiles);
            UpdateClubTaskCommand command = _mapper.Map<UpdateClubTaskCommand>(updateClubTaskDto);
            command.Attachments = postAttachments.Count > 0 ? JsonConvert.SerializeObject(postAttachments) : null;
            ClubTaskViewModel task = await Mediator.Send(command);
            if (oldAttachments != null)
            {
                List<Attachment> oldFiles = JsonConvert.DeserializeObject<List<Attachment>>(oldAttachments);
                List<Attachment> currentFiles = JsonConvert.DeserializeObject<List<Attachment>>(actualAttachments);
                List<Attachment> filesToDelete = oldFiles.Except(currentFiles).ToList();
                DeleteAttachments(filesToDelete);
            }
            return Ok(task);
        }

        /// <summary>
        /// Удаляет задание по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /clubtasks/1
        /// </remarks>
        /// <param name="id">id задания (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Задание успешно удалено</response>
        /// <response code="404">Задание не найдено</response>
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
            DeleteClubTaskCommand command = new DeleteClubTaskCommand()
            {
                Id = id,
                IssuerId = requesterId
            };
            ClubTaskViewModel task = await Mediator.Send(command);
            string attachmetsData = task.Attachments;
            if (attachmetsData != null)
            {
                List<Attachment> filesToDelete = JsonConvert.DeserializeObject<List<Attachment>>(attachmetsData);
                DeleteAttachments(filesToDelete);
            }
            return NoContent();
        }

        private List<Attachment> ProcessNewFiles(List<IFormFile> files, Guid postAttachmentsUid)
        {
            if (files == null)
                return null;
            List<Attachment> attachments = new List<Attachment>();
            string filePath = string.Empty;
            string savePath = string.Empty;
            string savePathTargetDirectory = string.Empty;
            foreach (var file in files)
            {
                filePath = $"post_{postAttachmentsUid}_{file.FileName}";
                savePathTargetDirectory = file.ContentType.ToLower().StartsWith("image") ? "images" : "miscellaneous";
                savePath = Path.Combine(_webHostEnvironment.ContentRootPath, $"uploads/posts-media/{savePathTargetDirectory}", filePath);
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                attachments.Add(new Attachment()
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    Length = file.Length,
                    Path = $"{_appDomain}uploads/posts-media/{savePathTargetDirectory}/{filePath}",
                    Extension = Path.GetExtension(filePath),
                });
            }
            return attachments;
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
