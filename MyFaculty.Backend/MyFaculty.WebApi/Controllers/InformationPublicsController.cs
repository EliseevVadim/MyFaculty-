using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFaculty.Application.Features.InformationPublics.Commands.BlockUserAtInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.CreateInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.DeleteInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.JoinInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.LeaveInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.UnblockUserAtInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Commands.UpdateInformationPublic;
using MyFaculty.Application.Features.InformationPublics.Queries.GetInformationPublicInfo;
using MyFaculty.Application.Features.InformationPublics.Queries.GetInformationPublics;
using MyFaculty.Application.Features.InformationPublics.Queries.GetInformationPublicsByName;
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
    public class InformationPublicsController : BaseController
    {
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;
        private string _appDomain;

        public InformationPublicsController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _appDomain = configuration["AppDomain"];
        }

        /// <summary>
        /// Gets the list of information publics
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /informationpublics
        /// </remarks>
        /// <returns>Returns InformationPublicsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<InformationPublicsListViewModel>> GetAll()
        {
            GetInformationPublicsQuery query = new GetInformationPublicsQuery();
            InformationPublicsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the list of information publics by name
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /informationpublics/search/name
        /// </remarks>
        /// <returns>Returns InformationPublicsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet("search/{request}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// Gets the information public by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /informationpublics/1
        /// </remarks>
        /// <param name="id">Information public id (integer)</param>
        /// <returns>Returns InformationPublicViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Creates the information public
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /informationpublics
        /// {
        ///     "publicName": "string",
        ///     "description": "string",
        ///     "image": "file",
        ///     "ownerId": 1,
        ///     "issuerId": 1
        /// }
        /// </remarks>
        /// <param name="createInformationPublicDto">CreateInformationPublicDto object</param>
        /// <returns>Retruns InformationPublicViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
            InformationPublicViewModel teacher = await Mediator.Send(command);
            return Created(nameof(InformationPublicsController), teacher);
        }

        /// <summary>
        /// Joins the information public
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /informationpublics/join
        ///     "userId": 1,
        ///     "publicId": 1
        /// }
        /// </remarks>
        /// <param name="joinInformationPublicDto">JoinInformationPublicDto object</param>
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
        /// Leaves the information public
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /informationpublics/leave
        ///     "userId": 1,
        ///     "publicId": 1
        /// }
        /// </remarks>
        /// <param name="leaveInformationPublicDto">LeaveInformationPublicDto object</param>
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
        /// Blocks the user at the information public
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /informationpublics/block-user
        ///     "userId": 1,
        ///     "publicId": 1,
        ///     "issuerId": 2
        /// }
        /// </remarks>
        /// <param name="blockUserAtInformationPublicDto">BlockUserAtInformationPublicDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Server error</response>
        [HttpPost("block-user")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BlockUser([FromBody] BlockUserAtInformationPublicDto blockUserAtInformationPublicDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != blockUserAtInformationPublicDto.IssuerId)
                return Forbid();
            BlockUserAtInformationPublicCommand command = _mapper.Map<BlockUserAtInformationPublicCommand>(blockUserAtInformationPublicDto);
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Unblocks the user at the information public
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /informationpublics/unblock-user
        ///     "userId": 1,
        ///     "publicId": 1,
        ///     "issuerId": 2
        /// }
        /// </remarks>
        /// <param name="unblockUserAtInformationPublicDto">UnblockUserAtInformationPublicDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Server error</response>
        [HttpPost("unblock-user")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// Updates the information public
        /// </summary>
        /// <remarks>
        /// Sample request:
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
        /// <param name="updateInformationPublicDto">UpdateInformationPublicDto object</param>
        /// <returns>Retruns InformationPublicViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Deletes the information public by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /informationpublics/1
        /// </remarks>
        /// <param name="id">Study club id (integer)</param>
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
            DeleteInformationPublicCommand command = new DeleteInformationPublicCommand()
            {
                Id = id,
                IssuerId = requesterId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
