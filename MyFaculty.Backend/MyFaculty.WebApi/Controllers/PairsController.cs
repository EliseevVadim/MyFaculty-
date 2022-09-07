using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Pairs.Commands.CreatePair;
using MyFaculty.Application.Features.Pairs.Commands.DeletePair;
using MyFaculty.Application.Features.Pairs.Commands.UpdatePair;
using MyFaculty.Application.Features.Pairs.Queries.GetPairInfo;
using MyFaculty.Application.Features.Pairs.Queries.GetPairs;
using MyFaculty.Application.Features.Pairs.Queries.GetPairsForGroup;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PairsController : BaseController
    {
        private IMapper _mapper;

        public PairsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of pairs
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /pairs
        /// </remarks>
        /// <returns>Returns PairsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PairsListViewModel>> GetAll()
        {
            GetPairsQuery query = new GetPairsQuery();
            PairsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the pair by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /pairs/1
        /// </remarks>
        /// <param name="id">Pair's id (integer)</param>
        /// <returns>Returns PairViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PairViewModel>> Get(int id)
        {
            GetPairInfoQuery query = new GetPairInfoQuery()
            {
                Id = id
            };
            PairViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the list of pairs for a specific group
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /pairs/group/1
        /// </remarks>
        /// <param name="id">Specific group's id (integer)</param>
        /// <returns>PairsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet("group/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PairsListViewModel>> GetByGroupId(int id)
        {
            GetPairsForGroupQuery query = new GetPairsForGroupQuery()
            {
                GroupId = id
            };
            PairsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the pair
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /pairs
        /// {
        ///     "pairName": "string",
        ///     "teacherId": 1,
        ///     "auditoriumId": 1,
        ///     "groupId": 1,
        ///     "disciplineId": 1,
        ///     "dayOfWeekId": 1,
        ///     "pairRepeatingId": 1,
        ///     "pairInfoId": 1
        /// }
        /// </remarks>
        /// <param name="createPairDto">CreatePairDto object</param>
        /// <returns>Retruns PairViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PairViewModel>> Create([FromBody] CreatePairDto createPairDto)
        {
            CreatePairCommand command = _mapper.Map<CreatePairCommand>(createPairDto);
            PairViewModel pair = await Mediator.Send(command);
            return Created(nameof(PairsController), pair);
        }

        /// <summary>
        /// Updates the pair
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /pairs
        /// {
        ///     "pairName": "string",
        ///     "teacherId": 1,
        ///     "auditoriumId": 1,
        ///     "groupId": 1,
        ///     "disciplineId": 1,
        ///     "dayOfWeekId": 1,
        ///     "pairRepeatingId": 1,
        ///     "pairInfoId": 1
        /// }
        /// </remarks>
        /// <param name="updatePairDto">UpdatePairDto object</param>
        /// <returns>Retruns PairViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PairViewModel>> Update([FromBody] UpdatePairDto updatePairDto)
        {
            UpdatePairCommand command = _mapper.Map<UpdatePairCommand>(updatePairDto);
            PairViewModel pair = await Mediator.Send(command);
            return Ok(pair);
        }

        /// <summary>
        /// Deletes the pair by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /pairs/1
        /// </remarks>
        /// <param name="id">Pair's id (integer)</param>
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
            DeletePairCommand command = new DeletePairCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
