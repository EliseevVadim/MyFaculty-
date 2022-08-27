using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.ScienceRanks.Commands.CreateScienceRank;
using MyFaculty.Application.Features.ScienceRanks.Commands.DeleteScienceRank;
using MyFaculty.Application.Features.ScienceRanks.Commands.UpdateScienceRank;
using MyFaculty.Application.Features.ScienceRanks.Queries.GetScienceRankInfo;
using MyFaculty.Application.Features.ScienceRanks.Queries.GetScienceRanks;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ScienceRanksController : BaseController
    {
        private IMapper _mapper;

        public ScienceRanksController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of science ranks
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /scienceranks
        /// </remarks>
        /// <returns>Returns ScienceRanksListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ScienceRanksListViewModel>> GetAll()
        {
            GetScienceRanksQuery query = new GetScienceRanksQuery();
            ScienceRanksListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the science rank by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /scienceranks/1
        /// </remarks>
        /// <param name="id">Science rank's id (integer)</param>
        /// <returns>Returns ScienceRankViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ScienceRankViewModel>> Get(int id)
        {
            GetScienceRankInfoQuery query = new GetScienceRankInfoQuery()
            {
                Id = id
            };
            ScienceRankViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the science rank
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /scienceranks
        /// {
        ///     "rankName": "string",
        /// }
        /// </remarks>
        /// <param name="createScienceRankDto">CreateScienceRankDto object</param>
        /// <returns>Retruns ScienceRankViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ScienceRankViewModel>> Create([FromBody] CreateScienceRankDto createScienceRankDto)
        {
            CreateScienceRankCommand command = _mapper.Map<CreateScienceRankCommand>(createScienceRankDto);
            ScienceRankViewModel rank = await Mediator.Send(command);
            return Created(nameof(ScienceRanksController), rank);
        }

        /// <summary>
        /// Updates the science rank
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /scienceranks
        /// {
        ///     "id": 1,
        ///     "rankName": "string",
        /// }
        /// </remarks>
        /// <param name="updateScienceRankDto">UpdateScienceRankDto object</param>
        /// <returns>Retruns ScienceRankViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ScienceRankViewModel>> Update([FromBody] UpdateScienceRankDto updateScienceRankDto)
        {
            UpdateScienceRankCommand command = _mapper.Map<UpdateScienceRankCommand>(updateScienceRankDto);
            ScienceRankViewModel rank = await Mediator.Send(command);
            return Ok(rank);
        }

        /// <summary>
        /// Deletes the science rank by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /scienceranks/1
        /// </remarks>
        /// <param name="id">Science rank's id (integer)</param>
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
            DeleteScienceRankCommand command = new DeleteScienceRankCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
