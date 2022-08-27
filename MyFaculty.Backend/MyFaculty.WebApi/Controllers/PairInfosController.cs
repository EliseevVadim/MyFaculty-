using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.PairInfos.Commands.CreatePairInfo;
using MyFaculty.Application.Features.PairInfos.Commands.DeletePairInfo;
using MyFaculty.Application.Features.PairInfos.Commands.UpdatePairInfo;
using MyFaculty.Application.Features.PairInfos.Queries.GetPairInfoDetails;
using MyFaculty.Application.Features.PairInfos.Queries.GetPairInfos;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PairInfosController : BaseController
    {
        private IMapper _mapper;

        public PairInfosController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of pair infos
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /pairinfos
        /// </remarks>
        /// <returns>Returns PairInfosListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PairInfosListViewModel>> GetAll()
        {
            GetPairInfosQuery query = new GetPairInfosQuery();
            PairInfosListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the pair info by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /pairinfos/1
        /// </remarks>
        /// <param name="id">Pair info's id (integer)</param>
        /// <returns>Returns PairInfoViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PairInfoViewModel>> Get(int id)
        {
            GetPairInfoDetailsQuery query = new GetPairInfoDetailsQuery()
            {
                Id = id
            };
            PairInfoViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the pair info
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /pairinfos
        /// {
        ///     "pairNumber": 1,
        ///     "startTime": "string",
        ///     "endTime": "string"
        /// }
        /// </remarks>
        /// <param name="createPairInfoDto">CreatePairInfoDto object</param>
        /// <returns>Retruns PairInfoViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PairInfoViewModel>> Create([FromBody] CreatePairInfoDto createPairInfoDto)
        {
            CreatePairInfoCommand command = _mapper.Map<CreatePairInfoCommand>(createPairInfoDto);
            PairInfoViewModel pairInfo = await Mediator.Send(command);
            return Created(nameof(PairInfosController), pairInfo);
        }

        /// <summary>
        /// Updates the pair info
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /pairinfos
        /// {
        ///     "id": 1,
        ///     "pairNumber": 1,
        ///     "startTime": "string",
        ///     "endTime": "string"
        /// }
        /// </remarks>
        /// <param name="updatePairInfoDto">UpdatePairInfoDto object</param>
        /// <returns>Retruns PairInfoViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PairInfoViewModel>> Update([FromBody] UpdatePairInfoDto updatePairInfoDto)
        {
            UpdatePairInfoCommand command = _mapper.Map<UpdatePairInfoCommand>(updatePairInfoDto);
            PairInfoViewModel pairInfo = await Mediator.Send(command);
            return Ok(pairInfo);
        }

        /// <summary>
        /// Deletes the pair info by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /pairinfos/1
        /// </remarks>
        /// <param name="id">Pair info's id (integer)</param>
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
            DeletePairInfoCommand command = new DeletePairInfoCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
