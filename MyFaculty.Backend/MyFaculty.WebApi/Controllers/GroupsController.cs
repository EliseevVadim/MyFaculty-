using AutoMapper;
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
    [Route("api/[controller]")]
    public class GroupsController : BaseController
    {
        private IMapper _mapper;

        public GroupsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GroupsListViewModel>> GetAll()
        {
            GetGroupsQuery query = new GetGroupsQuery();
            GroupsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupViewModel>> Get(int id)
        {
            GetGroupInfoQuery query = new GetGroupInfoQuery()
            {
                Id = id
            };
            GroupViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<GroupViewModel>> Create([FromBody] CreateGroupDto createGroupDto)
        {
            CreateGroupCommand command = _mapper.Map<CreateGroupCommand>(createGroupDto);
            GroupViewModel group = await Mediator.Send(command);
            return Created(nameof(GroupsController), group);
        }

        [HttpPut]
        public async Task<ActionResult<GroupViewModel>> Update([FromBody] UpdateGroupDto updateGroupDto)
        {
            UpdateGroupCommand command = _mapper.Map<UpdateGroupCommand>(updateGroupDto);
            GroupViewModel group = await Mediator.Send(command);
            return Ok(group);
        }

        [HttpDelete("{id}")]
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
