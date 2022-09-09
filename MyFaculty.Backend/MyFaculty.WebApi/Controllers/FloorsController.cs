using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FloorsController : BaseController
    {
        private IMapper _mapper;

        public FloorsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of floors
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /floors
        /// </remarks>
        /// <returns>Returns FloorsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<FloorsListViewModel>> GetAll()
        {
            GetFloorsQuery query = new GetFloorsQuery();
            FloorsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the floor by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /floors/1
        /// </remarks>
        /// <param name="id">Floor's id (integer)</param>
        /// <returns>Returns FloorViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FloorViewModel>> Get(int id)
        {
            GetFloorInfoQuery query = new GetFloorInfoQuery()
            {
                Id = id
            };
            FloorViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the floor
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /floors
        /// {
        ///     "name": "string",
        ///     "bounds": "json"
        /// }
        /// </remarks>
        /// <param name="createFloorDto">CreateFloorDto object</param>
        /// <returns>Retruns FloorViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FloorViewModel>> Create([FromBody] CreateFloorDto createFloorDto)
        {
            CreateFloorCommand command = _mapper.Map<CreateFloorCommand>(createFloorDto);
            FloorViewModel floor = await Mediator.Send(command);
            return Created(nameof(FloorsController), floor);
        }

        /// <summary>
        /// Updates the floor
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /floors
        /// {
        ///     "id": 1,
        ///     "name": "string",
        ///     "bounds": "json"
        /// }
        /// </remarks>
        /// <param name="updateFloorDto">UpdateFloorDto object</param>
        /// <returns>Retruns FloorViewModel</returns>
        /// <response code="200">Created</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FloorViewModel>> Update([FromBody] UpdateFloorDto updateFloorDto)
        {
            UpdateFloorCommand command = _mapper.Map<UpdateFloorCommand>(updateFloorDto);
            FloorViewModel floor = await Mediator.Send(command);
            return Ok(floor);
        }

        /// <summary>
        /// Deletes the floor by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /floors/1
        /// </remarks>
        /// <param name="id">Floor's id (integer)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
