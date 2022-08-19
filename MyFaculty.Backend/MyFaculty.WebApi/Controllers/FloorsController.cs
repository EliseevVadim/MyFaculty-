using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Floors.Commands.CreateFloor;
using MyFaculty.Application.Features.Floors.Commands.DeleteFloor;
using MyFaculty.Application.Features.Floors.Commands.UpdateFloor;
using MyFaculty.Application.Features.Floors.Queries.GetFloorInfo;
using MyFaculty.Application.Features.Floors.Queries.GetFloors;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class FloorsController : BaseController
    {
        private IMapper _mapper;

        public FloorsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<FloorsListViewModel>> GetAll()
        {
            GetFloorsQuery query = new GetFloorsQuery();
            FloorsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FloorViewModel>> Get(int id)
        {
            GetFloorInfoQuery query = new GetFloorInfoQuery()
            {
                Id = id
            };
            FloorViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<FloorViewModel>> Create([FromBody] CreateFloorDto createFloorDto)
        {
            CreateFloorCommand command = _mapper.Map<CreateFloorCommand>(createFloorDto);
            FloorViewModel floor = await Mediator.Send(command);
            return Created(nameof(FloorsController), floor);
        }

        [HttpPut]
        public async Task<ActionResult<FloorViewModel>> Update([FromBody] UpdateFloorDto updateFloorDto)
        {
            UpdateFloorCommand command = _mapper.Map<UpdateFloorCommand>(updateFloorDto);
            FloorViewModel floor = await Mediator.Send(command);
            return Ok(floor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteFloorCommand command = new DeleteFloorCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
