using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFaculty.Application.Features.InfoPosts.Commands.CreateInfoPost;
using MyFaculty.Application.Features.InfoPosts.Commands.DeleteInfoPost;
using MyFaculty.Application.Features.InfoPosts.Commands.LikeInfoPost;
using MyFaculty.Application.Features.InfoPosts.Commands.UnlikeInfoPost;
using MyFaculty.Application.Features.InfoPosts.Commands.UpdateInfoPost;
using MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPost;
using MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPostsByInformationPublic;
using MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPostsByStudyClub;
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
    public class InfoPostsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly string _appDomain;

        public InfoPostsController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _appDomain = configuration["AppDomain"];
        }

        /// <summary>
        /// Возвращает список записей, принадлежащих указанному информационному сообществу
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /infoposts/info-public/1
        /// </remarks>
        /// <param name="id">id информационного сообщества (integer)</param>
        /// <returns>Возвращает объект InfoPostsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Информационное сообщество не найдено</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        [HttpGet("info-public/{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<InfoPostsListViewModel>> GetByInfoPublic(int id)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetInfoPostsByInformationPublicQuery query = new GetInfoPostsByInformationPublicQuery()
            {
                PublicId = id,
                IssuerId = requesterId
            };
            InfoPostsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список записей, принадлежащих указанному сообществу курса
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /infoposts/study-club/1
        /// </remarks>
        /// <param name="id">id сообщества курса (integer)</param>
        /// <returns>Возвращает объект InfoPostsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("study-club/{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InfoPostsListViewModel>> GetByStudyClub(int id)
        {
            GetInfoPostsByStudyClubQuery query = new GetInfoPostsByStudyClubQuery()
            {
                StudyClubId = id
            };
            InfoPostsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информационную запись по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:  
        /// GET /infoposts/1
        /// </remarks>
        /// <param name="id">id записи (integer)</param>
        /// <returns>Возвращает объект InfoPostViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Запись не найдена</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<InfoPostViewModel>> Get(int id)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetInfoPostQuery query = new GetInfoPostQuery()
            {
                Id = id,
                IssuerId = requesterId
            };
            InfoPostViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новую запись
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /infoposts
        /// {
        ///     "textContent": "string",
        ///     "postAttachments": "list of files",
        ///     "studyClubId": null,
        ///     "infoPublicId": 1,
        ///     "authorId": 1,
        ///     "commentsAllowed": true
        /// }
        /// </remarks>
        /// <param name="createInfoPostDto">Объект CreateInfoPostDto</param>
        /// <returns>Возвращает объект InfoPostViewModel</returns>
        /// <response code="201">Запись успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InfoPostViewModel>> Create([FromForm] CreateInfoPostDto createInfoPostDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != createInfoPostDto.AuthorId)
                return Forbid();
            List<Attachment> postAttachments = new List<Attachment>();
            Guid postAttachmentsUid = Guid.NewGuid();
            List<Attachment> newFiles = ProcessNewFiles(createInfoPostDto.PostAttachments, postAttachmentsUid);
            if (newFiles != null)
                postAttachments.AddRange(newFiles);
            CreateInfoPostCommand command = _mapper.Map<CreateInfoPostCommand>(createInfoPostDto);
            command.Attachments = postAttachments.Count > 0 ? JsonConvert.SerializeObject(postAttachments) : null;
            command.PostAttachmentsUid = postAttachmentsUid;
            InfoPostViewModel infoPost = await Mediator.Send(command);
            return Created(nameof(InfoPostsController), infoPost);
        }

        /// <summary>
        /// Ставит отметку "Нравится" к записи
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /infoposts/like
        ///     "likedUserId": 1,
        ///     "likedPostId": 1
        /// }
        /// </remarks>
        /// <param name="likeInfoPostDto">Объект LikeInfoPostDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Запись успешно оценена</response>
        /// <response code="404">Запись не найдена</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("like")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Join([FromBody] LikeInfoPostDto likeInfoPostDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != likeInfoPostDto.LikedUserId)
                return Forbid();
            LikeInfoPostCommand command = _mapper.Map<LikeInfoPostCommand>(likeInfoPostDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Снимает отметку "Нравится" с записи
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /infoposts/unlike
        ///     "likedUserId": 1,
        ///     "likedPostId": 1
        /// }
        /// </remarks>
        /// <param name="unlikeInfoPostDto">Объект UnlikeInfoPostDto</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Отметка "Нравится" успешно снята</response>
        /// <response code="404">Запись не найдена</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="403">Действие запрещено</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="409">Действие не совместимо с состоянием системы</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost("unlike")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Leave([FromBody] UnlikeInfoPostDto unlikeInfoPostDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != unlikeInfoPostDto.LikedUserId)
                return Forbid();
            UnlikeInfoPostCommand command = _mapper.Map<UnlikeInfoPostCommand>(unlikeInfoPostDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Редактирует информационную запись
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /infoposts
        /// {
        ///     "infoPostId": 1,
        ///     "textContent": "string",
        ///     "oldAttachments": "json",
        ///     "actualAttachments": "json",
        ///     "newFiles": "list of files",
        ///     "postAttachmentsUid": "guid"
        ///     "issuerId": 1
        /// }
        /// </remarks>
        /// <param name="updateInfoPostDto">Объект UpdateInfoPostDto</param>
        /// <returns>Возвращает объект InfoPostViewModel</returns>
        /// <response code="200">Запись успешно обновлена</response>
        /// <response code="404">Запись не найдена</response>
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
        public async Task<ActionResult<InfoPostViewModel>> Update([FromForm] UpdateInfoPostDto updateInfoPostDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != updateInfoPostDto.IssuerId)
                return Forbid();
            string oldAttachments = updateInfoPostDto.OldAttachments;
            string actualAttachments = updateInfoPostDto.ActualAttachments;
            List<Attachment> postAttachments = actualAttachments == null ? new List<Attachment>() : JsonConvert.DeserializeObject<List<Attachment>>(actualAttachments);
            List<Attachment> newFiles = ProcessNewFiles(updateInfoPostDto.NewFiles, updateInfoPostDto.PostAttachmentsUid);
            if (newFiles != null)
                postAttachments.AddRange(newFiles);
            UpdateInfoPostCommand command = _mapper.Map<UpdateInfoPostCommand>(updateInfoPostDto);
            command.Attachments = postAttachments.Count > 0 ? JsonConvert.SerializeObject(postAttachments) : null;
            InfoPostViewModel infoPost = await Mediator.Send(command);
            if (oldAttachments != null)
            {
                List<Attachment> oldFiles = JsonConvert.DeserializeObject<List<Attachment>>(oldAttachments);
                List<Attachment> currentFiles = JsonConvert.DeserializeObject<List<Attachment>>(actualAttachments);
                List<Attachment> filesToDelete = oldFiles.Except(currentFiles).ToList();
                DeleteAttachments(filesToDelete);
            }
            return Ok(infoPost);
        }

        /// <summary>
        /// Удаляет запись по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /infoposts/1
        /// </remarks>
        /// <param name="id">id записи (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Задание успешно удалено</response>
        /// <response code="404">Задание не найдено</response>
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
            DeleteInfoPostCommand command = new DeleteInfoPostCommand()
            {
                Id = id,
                IssuerId = requesterId
            };
            InfoPostViewModel infoPost = await Mediator.Send(command);
            string attachmetsData = infoPost.Attachments;
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
