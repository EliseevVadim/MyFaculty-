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
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
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
        /// POST /studyclubs/leave
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
        /// Add the moderator to study club
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /studyclubs/add-moderator
        ///     "issuerId": 1,
        ///     "moderatorId": 2,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="addModeratorToStudyClubDto">AddModeratorToStudyClubDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Server error</response>
        [HttpPost("add-moderator")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddModerator([FromBody] AddModeratorToStudyClubDto addModeratorToStudyClubDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != addModeratorToStudyClubDto.IssuerId)
                return Forbid();
            AddModeratorToStudyClubCommand command = _mapper.Map<AddModeratorToStudyClubCommand>(addModeratorToStudyClubDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Demotes the moderator at study club
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /studyclubs/demote-moderator
        ///     "issuerId": 1,
        ///     "moderatorId": 2,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="demoteStudyClubModeratorDto">DemoteStudyClubModeratorDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Server error</response>
        [HttpPost("demote-moderator")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DemoteModerator([FromBody] DemoteStudyClubModeratorDto demoteStudyClubModeratorDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != demoteStudyClubModeratorDto.IssuerId)
                return Forbid();
            DemoteStudyClubModeratorCommand command = _mapper.Map<DemoteStudyClubModeratorCommand>(demoteStudyClubModeratorDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Removes the user from study club
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /studyclubs/remove-user
        ///     "issuerId": 1,
        ///     "removingUserId": 2,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="removeUserFromStudyClubDto">RemoveUserFromStudyClubDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Server error</response>
        [HttpPost("remove-user")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// Adds users from specific group to study club
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /studyclubs/add-users-from-group
        ///     "issuerId": 1,
        ///     "groupId": 2,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="addUsersToStudyClubByGroupDto">AddUsersToStudyClubByGroupDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Server error</response>
        [HttpPost("add-users-from-group")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// Adds users from specific course to study club
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /studyclubs/add-users-from-course
        ///     "issuerId": 1,
        ///     "courseId": 2,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="addUsersToStudyClubByCourseDto">AddUsersToStudyClubByCourseDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Server error</response>
        [HttpPost("add-users-from-course")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// Removes users from specific group from study club
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /studyclubs/remove-users-from-group
        ///     "issuerId": 1,
        ///     "groupId": 2,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="removeUsersFromStudyClubByGroupDto">RemoveUsersFromStudyClubByGroupDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Server error</response>
        [HttpPost("remove-users-from-group")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// Adds users from specific course to study club
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /studyclubs/remove-users-from-course
        ///     "issuerId": 1,
        ///     "courseId": 2,
        ///     "studyClubId": 1
        /// }
        /// </remarks>
        /// <param name="removeUsersFromStudyClubByCourseDto">RemoveUsersFromStudyClubByCourseDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Server error</response>
        [HttpPost("remove-users-from-course")]
        [Authorize(Roles = "Teacher")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
