using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Groups.Commands.CreateGroup;
using MyFaculty.Application.Features.Groups.Commands.DeleteGroup;
using MyFaculty.Application.Features.Groups.Commands.UpdateGroup;
using MyFaculty.Application.Features.Groups.Queries.GetGroupInfo;
using MyFaculty.Application.Features.Groups.Queries.GetGroups;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class GroupsController : BaseController
    {
        private IMapper _mapper;

        public GroupsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of groups
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /groups
        /// </remarks>
        /// <returns>Returns GroupsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GroupsListViewModel>> GetAll()
        {
            GetGroupsQuery query = new GetGroupsQuery();
            GroupsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the group by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /groups/1
        /// </remarks>
        /// <param name="id">Group's id (integer)</param>
        /// <returns>Returns GroupViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GroupViewModel>> Get(int id)
        {
            GetGroupInfoQuery query = new GetGroupInfoQuery()
            {
                Id = id
            };
            GroupViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the group
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /groups
        /// {
        ///     "groupName": "string",
        ///     "courseId": 1
        /// }
        /// </remarks>
        /// <param name="createGroupDto">CreateGroupDto object</param>
        /// <returns>Retruns GroupViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GroupViewModel>> Create([FromBody] CreateGroupDto createGroupDto)
        {
            CreateGroupCommand command = _mapper.Map<CreateGroupCommand>(createGroupDto);
            GroupViewModel group = await Mediator.Send(command);
            return Created(nameof(GroupsController), group);
        }

        /// <summary>
        /// Updates the group
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /groups
        /// {
        ///     "id": 1,
        ///     "groupName": "string",
        ///     "courseId": 1
        /// }
        /// </remarks>
        /// <param name="updateGroupDto">CreateGroupDto object</param>
        /// <returns>Retruns GroupViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GroupViewModel>> Update([FromBody] UpdateGroupDto updateGroupDto)
        {
            UpdateGroupCommand command = _mapper.Map<UpdateGroupCommand>(updateGroupDto);
            GroupViewModel group = await Mediator.Send(command);
            return Ok(group);
        }

        /// <summary>
        /// Deletes the group by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /groups/1
        /// </remarks>
        /// <param name="id">Group's id (integer)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteGroupCommand command = new DeleteGroupCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
