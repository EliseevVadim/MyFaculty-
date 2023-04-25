using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFaculty.Application.Features.Users.Commands.BanUser;
using MyFaculty.Application.Features.Users.Commands.TransferUsersToAnotherGroup;
using MyFaculty.Application.Features.Users.Commands.UnbanUser;
using MyFaculty.Application.Features.Users.Commands.UpdateUser;
using MyFaculty.Application.Features.Users.Queries.GetUserInfo;
using MyFaculty.Application.Features.Users.Queries.GetUsers;
using MyFaculty.Application.Features.Users.Queries.GetUsersForGroup;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using MyFaculty.WebApi.Services;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;
        private string _appDomain;

        public UsersController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _appDomain = configuration["AppDomain"];
        }

        /// <summary>
        /// Gets the list of users
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /users
        /// </remarks>
        /// <returns>Returns UsersListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UsersListViewModel>> GetAll()
        {
            GetUsersQuery query = new GetUsersQuery();
            UsersListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the list of users for a specific group
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /users/group/1
        /// </remarks>
        /// <returns>Returns UsersListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet("group/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UsersListViewModel>> GetByGroupId(int id)
        {
            GetUsersForGroupQuery query = new GetUsersForGroupQuery()
            {
                GroupId = id
            };
            UsersListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the user by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /users/1
        /// </remarks>
        /// <param name="id">User's id (integer)</param>
        /// <returns>Returns UserViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserViewModel>> Get(int id)
        {
            GetUserInfoQuery query = new GetUserInfoQuery()
            {
                Id = id
            };
            UserViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Updates the user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /users
        /// {
        ///     "id": 1,
        ///     "firstName": "string",
        ///     "lastName": "string",
        ///     "photo": "file",
        ///     "email": "string",
        ///     "cityId": 1,
        ///     "website": "https://google.com",
        ///     "vkLink": "https://vk.com/id1",
        ///     "telegramLink": "https://t.me/durov"
        /// }
        /// </remarks>
        /// <param name="updateUserDto">UpdateUserDto object</param>
        /// <returns>Retruns UserViewModel</returns>
        /// <response code="201">Created</response>
        /// /// <response code="403">Forbidden</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserViewModel>> Update([FromForm] UpdateUserDto updateUserDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (requesterId != updateUserDto.Id)
                return Forbid();
            string photoPath = string.Empty;
            string savePath = string.Empty;            
            if (updateUserDto.Photo != null)
            {
                photoPath = Guid.NewGuid().ToString() + "_" + updateUserDto.Photo.FileName;
                savePath = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads/images/user_avatars", photoPath);
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    updateUserDto.Photo.CopyTo(stream);
                }
            }
            UpdateUserCommand command = _mapper.Map<UpdateUserCommand>(updateUserDto);
            command.AvatarPath = String.IsNullOrEmpty(photoPath) ? string.Empty : _appDomain + "uploads/images/user_avatars/" + photoPath;
            UserViewModel user = await Mediator.Send(command);
            return Ok(user);
        }

        /// <summary>
        /// Transfers the users from one group to another
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /users/groupTransfer
        /// {
        ///     "sourceGroupId": 1,
        ///     "destinationGroupId": 2
        /// }
        /// </remarks>
        /// <param name="transferUsersToAnotherGroupDto">TransferUsersToAnotherGroupDto object</param>
        /// <returns>Retruns UserViewModel</returns>
        /// <response code="200">Success</response>
        /// /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut("groupTransfer")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TransferUsersToGroup([FromBody] TransferUsersToAnotherGroupDto transferUsersToAnotherGroupDto)
        {
            TransferUsersToAnotherGroupCommand command = _mapper.Map<TransferUsersToAnotherGroupCommand>(transferUsersToAnotherGroupDto);
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Bans the user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /users/ban
        /// {
        ///     "bannedUserId": 2,
        ///     "administratorId": 1,
        ///     "reason": "spam"
        /// }
        /// </remarks>
        /// <param name="banUserDto">BanUserDto object</param>
        /// <returns>Retruns OkResult</returns>
        /// <response code="200">Sucess</response>
        /// <response code="403">Forbidden</response> 
        /// <response code="500">Server error</response>
        [HttpPost("ban")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Ban([FromBody] BanUserDto banUserDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            BanUserCommand command = _mapper.Map<BanUserCommand>(banUserDto);
            command.AdministratorId = requesterId;
            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Unbans the user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /users/unban
        /// {
        ///     "unbannedUserId": 2,
        ///     "administratorId": 1,
        ///     "reason": "amnestied"
        /// }
        /// </remarks>
        /// <param name="unbanUserDto">UnbanUserDto object</param>
        /// <returns>Retruns OkResult</returns>
        /// <response code="200">Sucess</response>
        /// <response code="403">Forbidden</response> 
        /// <response code="500">Server error</response>
        [HttpPost("unban")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Unban([FromBody] UnbanUserDto unbanUserDto)
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UnbanUserCommand command = _mapper.Map<UnbanUserCommand>(unbanUserDto);
            command.AdministratorId = requesterId;
            int unbanedUserId = await Mediator.Send(command);
            RolesService rolesService = new RolesService();
            await rolesService.AddUserToRoleAsync(unbanedUserId, "User");
            return Ok();
        }
    }
}
