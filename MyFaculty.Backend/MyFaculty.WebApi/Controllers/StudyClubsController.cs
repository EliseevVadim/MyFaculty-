using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFaculty.Application.Features.StudyClubs.Commands.CreateStudyClub;
using MyFaculty.Application.Features.StudyClubs.Commands.DeleteStudyClub;
using MyFaculty.Application.Features.StudyClubs.Commands.JoinStudyClub;
using MyFaculty.Application.Features.StudyClubs.Commands.LeaveStudyClub;
using MyFaculty.Application.Features.StudyClubs.Commands.UpdateStudyClub;
using MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubInfo;
using MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubs;
using MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubsByName;
using MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubsForSpecificUser;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
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
        private string _appDomain;

        public StudyClubsController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _appDomain = configuration["AppDomain"];
        }

        /// <summary>
        /// Gets the list of study clubs
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /studyclubs
        /// </remarks>
        /// <returns>Returns StudyClubsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StudyClubsListViewModel>> GetAll()
        {
            GetStudyClubsQuery query = new GetStudyClubsQuery();
            StudyClubsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the list of study clubs where specific user are member of
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /studyclubs/user/1
        /// </remarks>
        /// <returns>Returns StudyClubsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet("user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// Gets the list of study clubs by name
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /studyclubs/search/name
        /// </remarks>
        /// <returns>Returns StudyClubsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet("search/{request}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// Gets the study club by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /studyclubs/1
        /// </remarks>
        /// <param name="id">Study club id (integer)</param>
        /// <returns>Returns StudyClubViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Creates the study club
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /studyclubs
        /// {
        ///     "studyClubName": "string",
        ///     "description": "string",
        ///     "image": "file",
        ///     "ownerId": 1
        /// }
        /// </remarks>
        /// <param name="createStudyClubDto">CreateStudyClubDto object</param>
        /// <returns>Retruns StudyClubViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudyClubViewModel>> Create([FromForm] CreateStudyClubDto createStudyClubDto)
        {
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
            StudyClubViewModel teacher = await Mediator.Send(command);
            return Created(nameof(StudyClubsController), teacher);
        }

        /// <summary>
        /// Joins the study club
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /studyclubs/join
        ///     "userId": 1,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="joinStudyClubDto">JoinStudyClubDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Server error</response>
        [HttpPost("join")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// Leaves the study club
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /studyclubs/join
        ///     "userId": 1,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="leaveStudyClubDto">LeaveStudyClubDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Server error</response>
        [HttpPost("leave")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// Updates the study club
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /studyclubs
        /// {
        ///     "id": 1,
        ///     "studyClubName": "string",
        ///     "description": "string",
        ///     "image": "file",
        ///     "ownerId": 1
        /// }
        /// </remarks>
        /// <param name="updateStudyClubDto">UpdateStudyClubDto object</param>
        /// <returns>Retruns StudyClubViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudyClubViewModel>> Update([FromForm] UpdateStudyClubDto updateStudyClubDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != updateStudyClubDto.OwnerId)
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
            StudyClubViewModel teacher = await Mediator.Send(command);
            return Ok(teacher);
        }

        /// <summary>
        /// Deletes the study club by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /studyclubs/1
        /// </remarks>
        /// <param name="id">Study club id (integer)</param>
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
            DeleteStudyClubCommand command = new DeleteStudyClubCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
