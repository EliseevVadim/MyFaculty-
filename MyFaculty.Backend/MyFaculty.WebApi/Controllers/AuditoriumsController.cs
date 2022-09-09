using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Auditoriums.Commands.CreateAuditorium;
using MyFaculty.Application.Features.Auditoriums.Commands.DeleteAuditorium;
using MyFaculty.Application.Features.Auditoriums.Commands.UpdateAuditorium;
using MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriumInfo;
using MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriums;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuditoriumsController : BaseController
    {
        private IMapper _mapper;

        public AuditoriumsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of auditoriums
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /auditoriums
        /// </remarks>
        /// <returns>Returns AuditoriumsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuditoriumsListViewModel>> GetAll()
        {
            GetAuditoriumsQuery query = new GetAuditoriumsQuery();
            AuditoriumsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the auditorium by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /auditoriums/1
        /// </remarks>
        /// <param name="id">Auditorium's id (integer)</param>
        /// <returns>Returns AuditoriumViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuditoriumViewModel>> Get(int id)
        {
            GetAuditoriumInfoQuery query = new GetAuditoriumInfoQuery()
            {
                Id = id
            };
            AuditoriumViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the auditorium
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /auditoriums
        /// {
        ///     "auditoriumName": "string",
        ///     "positionInfo": "string",
        ///     "floorId": 0,
        ///     "teacherId": 0
        /// }
        /// </remarks>
        /// <param name="createAuditoriumDto">CreateAuditoriumDto object</param>
        /// <returns>Retruns AuditoriumViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuditoriumViewModel>> Create([FromBody] CreateAuditoriumDto createAuditoriumDto)
        {
            CreateAuditoriumCommand command = _mapper.Map<CreateAuditoriumCommand>(createAuditoriumDto);
            AuditoriumViewModel auditorium = await Mediator.Send(command);
            return Created(nameof(AuditoriumsController), auditorium);
        }

        /// <summary>
        /// Updates the auditorium
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /auditoriums
        /// {
        ///     "id": 1,
        ///     "auditoriumName": "string",
        ///     "positionInfo": "string",
        ///     "floorId": 0,
        ///     "teacherId": 0
        /// }        
        /// </remarks>
        /// <param name="updateAuditoriumDto">UpdateAuditoriumDto object</param>
        /// <returns>Returns AuditoriumViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>        
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuditoriumViewModel>> Update([FromBody] UpdateAuditoriumDto updateAuditoriumDto)
        {
            UpdateAuditoriumCommand command = _mapper.Map<UpdateAuditoriumCommand>(updateAuditoriumDto);
            AuditoriumViewModel course = await Mediator.Send(command);
            return Ok(course);
        }

        /// <summary>
        /// Deletes the auditorium by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /auditorium/1
        /// </remarks>
        /// <param name="id">Auditorium's id (integer)</param>
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
            DeleteAuditoriumCommand command = new DeleteAuditoriumCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
