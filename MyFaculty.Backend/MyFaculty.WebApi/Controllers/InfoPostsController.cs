using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFaculty.Application.ViewModels;
using MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPostsByInformationPublic;
using System.Threading.Tasks;
using MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPost;
using MyFaculty.WebApi.Dto;
using Microsoft.AspNetCore.Authorization;
using System;
using System.IO;
using System.Collections.Generic;
using MyFaculty.WebApi.Models;
using MyFaculty.Application.Features.InfoPosts.Commands.CreateInfoPost;
using Newtonsoft.Json;
using System.Security.Claims;
using MyFaculty.Application.Features.InfoPosts.Commands.DeleteInfoPost;
using MyFaculty.Application.Features.InfoPosts.Commands.UpdateInfoPost;
using System.Linq;
using MyFaculty.Application.Features.InfoPosts.Commands.LikeInfoPost;
using MyFaculty.Application.Features.InfoPosts.Commands.UnlikeInfoPost;
using MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPostsByStudyClub;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class InfoPostsController : BaseController
    {
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;
        private string _appDomain;

        public InfoPostsController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _appDomain = configuration["AppDomain"];
        }

        /// <summary>
        /// Gets the list of information posts by a specific information public
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /infoposts/info-public/1
        /// </remarks>
        /// <param name="id">Specific information public id (integer)</param>
        /// <returns>Returns InfoPostsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet("info-public/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<InfoPostsListViewModel>> GetByInfoPublic(int id)
        {
            GetInfoPostsByInformationPublicQuery query = new GetInfoPostsByInformationPublicQuery()
            {
                PublicId = id
            };
            InfoPostsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the list of information posts by a specific study club
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /infoposts/study-club/1
        /// </remarks>
        /// <param name="id">Specific study club id (integer)</param>
        /// <returns>Returns InfoPostsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet("study-club/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// Gets the information post by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /infoposts/1
        /// </remarks>
        /// <param name="id">Information post's id (integer)</param>
        /// <returns>Returns InfoPostViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InfoPostViewModel>> Get(int id)
        {
            GetInfoPostQuery query = new GetInfoPostQuery()
            {
                Id = id
            };
            InfoPostViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the information post
        /// </summary>
        /// <remarks>
        /// Sample request:
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
        /// <param name="createInfoPostDto">CreateInfoPostDto object</param>
        /// <returns>Retruns InfoPostViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InfoPostViewModel>> Create([FromForm] CreateInfoPostDto createInfoPostDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != createInfoPostDto.AuthorId)
                return Forbid();
            List<PostAttachment> postAttachments = new List<PostAttachment>(); 
            Guid postAttachmentsUid = Guid.NewGuid();
            List<PostAttachment> newFiles = ProcessNewFiles(createInfoPostDto.PostAttachments, postAttachmentsUid);
            if (newFiles != null)
                postAttachments.AddRange(newFiles);          
            CreateInfoPostCommand command = _mapper.Map<CreateInfoPostCommand>(createInfoPostDto);
            command.Attachments = postAttachments.Count > 0 ? JsonConvert.SerializeObject(postAttachments) : null;
            command.PostAttachmentsUid = postAttachmentsUid;
            InfoPostViewModel infoPost = await Mediator.Send(command);
            return Created(nameof(InfoPostsController), infoPost);
        }

        /// <summary>
        /// Likes the information post
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /infoposts/like
        ///     "likedUserId": 1,
        ///     "likedPostId": 1
        /// }
        /// </remarks>
        /// <param name="likeInfoPostDto">LikeInfoPostDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Server error</response>
        [HttpPost("like")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// Unlikes the information post
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /infoposts/unlike
        ///     "likedUserId": 1,
        ///     "likedPostId": 1
        /// }
        /// </remarks>
        /// <param name="unlikeInfoPostDto">UnlikeInfoPostDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Server error</response>
        [HttpPost("unlike")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// Updates the information post
        /// </summary>
        /// <remarks>
        /// Sample request:
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
        /// <param name="updateInfoPostDto">UpdateInfoPostDto object</param>
        /// <returns>Retruns InfoPostViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InfoPostViewModel>> Update([FromForm] UpdateInfoPostDto updateInfoPostDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != updateInfoPostDto.IssuerId)
                return Forbid();
            string oldAttachments = updateInfoPostDto.OldAttachments;
            string actualAttachments = updateInfoPostDto.ActualAttachments;
            List<PostAttachment> postAttachments = actualAttachments == null ? new List<PostAttachment>() : JsonConvert.DeserializeObject<List<PostAttachment>>(actualAttachments);
            List<PostAttachment> newFiles = ProcessNewFiles(updateInfoPostDto.NewFiles, updateInfoPostDto.PostAttachmentsUid);
            if (newFiles != null)
                postAttachments.AddRange(newFiles);
            UpdateInfoPostCommand command = _mapper.Map<UpdateInfoPostCommand>(updateInfoPostDto);
            command.Attachments = postAttachments.Count > 0 ? JsonConvert.SerializeObject(postAttachments) : null;
            InfoPostViewModel infoPost = await Mediator.Send(command);
            if (oldAttachments != null && infoPost.Attachments != null)
            {
                List<PostAttachment> oldFiles = JsonConvert.DeserializeObject<List<PostAttachment>>(oldAttachments);
                List<PostAttachment> currentFiles = JsonConvert.DeserializeObject<List<PostAttachment>>(actualAttachments);
                List<PostAttachment> filesToDelete = oldFiles.Except(currentFiles).ToList();
                DeleteAttachments(filesToDelete);
            }
            return Ok(infoPost);
        }

        /// <summary>
        /// Deletes the information post by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /infoposts/1
        /// </remarks>
        /// <param name="id">Information post id (integer)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
