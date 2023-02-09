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
        /// Gets the list of club tasks for a study club
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /clubtasks/study-club/1
        /// </remarks>
        /// <param name="id">Specific faculty id (integer)</param>
        /// <returns>ClubTasksListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet("study-club/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// Gets the study club task by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /clubtasks/1
        /// </remarks>
        /// <param name="id">Study club task's id (integer)</param>
        /// <returns>Returns ClubTaskViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Creates the study club task
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /clubtasks
        /// {
        ///     "textContent": "string",
        ///     "postAttachments": "list of files",
        ///     "studyClubId": 1,
        ///     "authorId": 1,
        ///     "deadLine": "future date time"
        /// }
        /// </remarks>
        /// <param name="createClubTaskDto">CreateClubTaskDto object</param>
        /// <returns>Retruns ClubTaskViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClubTaskViewModel>> Create([FromForm] CreateClubTaskDto createClubTaskDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != createClubTaskDto.AuthorId)
                return Forbid();
            List<PostAttachment> postAttachments = new List<PostAttachment>();
            Guid postAttachmentsUid = Guid.NewGuid();
            List<PostAttachment> newFiles = ProcessNewFiles(createClubTaskDto.PostAttachments, postAttachmentsUid);
            if (newFiles != null)
                postAttachments.AddRange(newFiles);
            CreateClubTaskCommand command = _mapper.Map<CreateClubTaskCommand>(createClubTaskDto);
            command.Attachments = postAttachments.Count > 0 ? JsonConvert.SerializeObject(postAttachments) : null;
            command.PostAttachmentsUid = postAttachmentsUid;
            ClubTaskViewModel task = await Mediator.Send(command);
            return Created(nameof(ClubTasksController), task);
        }

        /// <summary>
        /// Updates the study club task
        /// </summary>
        /// <remarks>
        /// Sample request:
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
        /// <param name="updateClubTaskDto">UpdateClubTaskDto object</param>
        /// <returns>Retruns ClubTaskViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClubTaskViewModel>> Update([FromForm] UpdateClubTaskDto updateClubTaskDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != updateClubTaskDto.IssuerId)
                return Forbid();
            string oldAttachments = updateClubTaskDto.OldAttachments;
            string actualAttachments = updateClubTaskDto.ActualAttachments;
            List<PostAttachment> postAttachments = actualAttachments == null ? new List<PostAttachment>() : JsonConvert.DeserializeObject<List<PostAttachment>>(actualAttachments);
            List<PostAttachment> newFiles = ProcessNewFiles(updateClubTaskDto.NewFiles, updateClubTaskDto.PostAttachmentsUid);
            if (newFiles != null)
                postAttachments.AddRange(newFiles);
            UpdateClubTaskCommand command = _mapper.Map<UpdateClubTaskCommand>(updateClubTaskDto);
            command.Attachments = postAttachments.Count > 0 ? JsonConvert.SerializeObject(postAttachments) : null;
            ClubTaskViewModel task = await Mediator.Send(command);
            if (oldAttachments != null && task.Attachments != null)
            {
                List<PostAttachment> oldFiles = JsonConvert.DeserializeObject<List<PostAttachment>>(oldAttachments);
                List<PostAttachment> currentFiles = JsonConvert.DeserializeObject<List<PostAttachment>>(actualAttachments);
                List<PostAttachment> filesToDelete = oldFiles.Except(currentFiles).ToList();
                DeleteAttachments(filesToDelete);
            }
            return Ok(task);
        }

        /// <summary>
        /// Deletes the study club task by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /clubtasks/1
        /// </remarks>
        /// <param name="id">Study club task id (integer)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
                List<PostAttachment> filesToDelete = JsonConvert.DeserializeObject<List<PostAttachment>>(attachmetsData);
                DeleteAttachments(filesToDelete);
            }
            return NoContent();
        }

        private List<PostAttachment> ProcessNewFiles(List<IFormFile> files, Guid postAttachmentsUid)
        {
            if (files == null)
                return null;
            List<PostAttachment> attachments = new List<PostAttachment>();
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
                attachments.Add(new PostAttachment()
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

        private void DeleteAttachments(List<PostAttachment> attachmentsToDelete)
        {
            foreach (PostAttachment attachment in attachmentsToDelete)
            {
                string path = attachment.Path
                    .Replace(_appDomain, $"{_webHostEnvironment.ContentRootPath}/");
                System.IO.File.Delete(path);
            }
        }
    }
}
