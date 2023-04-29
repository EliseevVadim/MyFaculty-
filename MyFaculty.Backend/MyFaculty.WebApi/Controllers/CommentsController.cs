using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFaculty.Application.Dto;
using MyFaculty.Application.Features.Comments.Commands.CreateComment;
using MyFaculty.Application.Features.Comments.Commands.DeleteComment;
using MyFaculty.Application.Features.Comments.Commands.UpdateComment;
using MyFaculty.Application.Features.Comments.Queries.GetCommentsForPost;
using MyFaculty.Application.Features.Comments.Queries.GetExcelDumpOfCommentsForPost;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using MyFaculty.WebApi.Hubs;
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
    public class CommentsController : BaseController
    {
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;
        private NotificationsHub _notificationsHub;
        private string _appDomain;

        public CommentsController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, NotificationsHub notificationsHub)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _appDomain = configuration["AppDomain"];
            _notificationsHub = notificationsHub;
        }

        /// <summary>
        /// Возвращает список комментариев к конкретной записи
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /comments/post/1
        /// </remarks>
        /// <param name="id">id записи (integer)</param>
        /// <returns>Возвращает объект InfoPostsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Запись не найдена</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        [HttpGet("post/{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CommentsListViewModel>> GetByPostId(int id)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetCommentsForPostQuery query = new GetCommentsForPostQuery()
            {
                PostId = id,
                IssuerId = requesterId
            };
            CommentsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Получает список комментариев к конкретной записи в виде файла excel
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /comments/excel/post/1
        /// </remarks>
        /// <param name="id">id записи (integer)</param>
        /// <returns>Возвращает объект File</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Запись не найдена</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response> 
        /// <response code="500">Внутренняя серверная ошибка</response> 
        [HttpGet("excel/post/{id}")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsExcelDumpByPostId(int id)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetExcelDumpOfCommentsForPostQuery query = new GetExcelDumpOfCommentsForPostQuery()
            {
                PostId = id,
                IssuerId = requesterId
            };
            ExcelFileInfoDto dto = await Mediator.Send(query);
            return File(dto.Content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "result.xlsx");
        }

        /// <summary>
        /// Создает новый комментарий
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /comments
        /// {
        ///     "textContent": "string",
        ///     "commentAttachments": "list of files",
        ///     "postId": 1,
        ///     "authorId": 1,
        ///     "parentCommentId": null
        /// }
        /// </remarks>
        /// <param name="createCommentDto">Объект CreateCommentDto</param>
        /// <returns>Возвращает объект CommentViewModel</returns>
        /// <response code="201">Комментарий успешно создан</response>
        /// <response code="404">Запись не найдена</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommentViewModel>> Create([FromForm] CreateCommentDto createCommentDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != createCommentDto.AuthorId)
                return Forbid();
            List<Attachment> commentAttachments = new List<Attachment>();
            Guid commentAttachmentsUid = Guid.NewGuid();
            List<Attachment> newFiles = ProcessNewFiles(createCommentDto.CommentAttachments, commentAttachmentsUid);
            if (newFiles != null)
                commentAttachments.AddRange(newFiles);
            CreateCommentCommand command = _mapper.Map<CreateCommentCommand>(createCommentDto);
            command.Attachments = commentAttachments.Count > 0 ? JsonConvert.SerializeObject(commentAttachments) : null;
            command.CommentAttachmentsUid = commentAttachmentsUid;
            CommentViewModel comment = await Mediator.Send(command);
            if (comment.ParentComment != null)
                await _notificationsHub.MakeUserLoadNotificationsAsync(comment.ParentComment.AuthorId);
            return Created(nameof(InfoPostsController), comment);
        }

        /// <summary>
        /// Редактирует комментарий
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /comments
        /// {
        ///     "commentId": 1,
        ///     "textContent": "string",
        ///     "oldAttachments": "json",
        ///     "actualAttachments": "json",
        ///     "newFiles": "list of files",
        ///     "commentAttachmentsUid": "guid"
        ///     "issuerId": 1
        /// }
        /// </remarks>
        /// <param name="updateCommentDto">Объект UpdateCommentDto</param>
        /// <returns>Возвращает объект CommentViewModel</returns>
        /// <response code="200">Комментарий успешно обновлен</response>
        /// <response code="404">Комментарий не найден</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPut]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CommentViewModel>> Update([FromForm] UpdateCommentDto updateCommentDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != updateCommentDto.IssuerId)
                return Forbid();
            string oldAttachments = updateCommentDto.OldAttachments;
            string actualAttachments = updateCommentDto.ActualAttachments;
            List<Attachment> commentAttachments = actualAttachments == null ? new List<Attachment>() : JsonConvert.DeserializeObject<List<Attachment>>(actualAttachments);
            List<Attachment> newFiles = ProcessNewFiles(updateCommentDto.NewFiles, updateCommentDto.CommentAttachmentsUid);
            if (newFiles != null)
                commentAttachments.AddRange(newFiles);
            UpdateCommentCommand command = _mapper.Map<UpdateCommentCommand>(updateCommentDto);
            command.Attachments = commentAttachments.Count > 0 ? JsonConvert.SerializeObject(commentAttachments) : null;
            CommentViewModel comment = await Mediator.Send(command);
            if (oldAttachments != null)
            {
                List<Attachment> oldFiles = JsonConvert.DeserializeObject<List<Attachment>>(oldAttachments);
                List<Attachment> currentFiles = JsonConvert.DeserializeObject<List<Attachment>>(actualAttachments);
                List<Attachment> filesToDelete = oldFiles.Except(currentFiles).ToList();
                DeleteAttachments(filesToDelete);
            }
            return Ok(comment);
        }

        /// <summary>
        /// Удаляет комментарий по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /comments/1
        /// </remarks>
        /// <param name="id">id комментария (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Комментарий успешно удален</response>
        /// <response code="404">Комментарий не найден</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            DeleteCommentCommand command = new DeleteCommentCommand()
            {
                Id = id,
                IssuerId = requesterId
            };
            CommentViewModel comment = await Mediator.Send(command);
            string attachmetsData = comment.Attachments;
            if (attachmetsData != null)
            {
                List<Attachment> filesToDelete = JsonConvert.DeserializeObject<List<Attachment>>(attachmetsData);
                DeleteAttachments(filesToDelete);
            }
            return NoContent();
        }

        private List<Attachment> ProcessNewFiles(List<IFormFile> files, Guid commentAttachmentsUid)
        {
            if (files == null)
                return null;
            List<Attachment> attachments = new List<Attachment>();
            string filePath = string.Empty;
            string savePath = string.Empty;
            string savePathTargetDirectory = string.Empty;
            foreach (var file in files)
            {
                filePath = $"comment_{commentAttachmentsUid}_{file.FileName}";
                savePathTargetDirectory = file.ContentType.ToLower().StartsWith("image") ? "images" : "miscellaneous";
                savePath = Path.Combine(_webHostEnvironment.ContentRootPath, $"uploads/comments-media/{savePathTargetDirectory}", filePath);
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                attachments.Add(new Attachment()
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    Length = file.Length,
                    Path = $"{_appDomain}uploads/comments-media/{savePathTargetDirectory}/{filePath}",
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
