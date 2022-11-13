using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Regions.Commands.CreateRegion;
using MyFaculty.Application.Features.Regions.Commands.DeleteRegion;
using MyFaculty.Application.Features.Regions.Commands.UpdateRegion;
using MyFaculty.Application.Features.Regions.Queries.GetRegions;
using MyFaculty.Application.Features.Regions.Queries.GetRegionsForCountry;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RegionsController : BaseController
    {
        private IMapper _mapper;

        public RegionsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of regions
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /regions
        /// </remarks>
        /// <returns>Returns RegionsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RegionsListViewModel>> GetAll()
        {
            GetRegionsQuery query = new GetRegionsQuery();
            RegionsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the list of regions for a specific country
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /regions/country/1
        /// </remarks>
        /// <param name="id">Specific country id (integer)</param>
        /// <returns>Returns RegionsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet("country/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RegionsListViewModel>> GetByCountryId(int id)
        {
            GetRegionsForCountryQuery query = new GetRegionsForCountryQuery()
            {
                CountryId = id
            };
            RegionsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the region
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /regions
        /// {
        ///     "regionName": "string",
        ///     "countryId": 0
        /// }
        /// </remarks>
        /// <param name="createRegionDto">CreateRegionDto object</param>
        /// <returns>Retruns RegionViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegionViewModel>> Create([FromBody] CreateRegionDto createRegionDto)
        {
            CreateRegionCommand command = _mapper.Map<CreateRegionCommand>(createRegionDto);
            RegionViewModel region = await Mediator.Send(command);
            return Created(nameof(RegionsController), region);
        }

        /// <summary>
        /// Updates the city
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /regions
        /// {
        ///     "id": 1,
        ///     "regionName": "string",
        ///     "countryId": 0
        /// }        
        /// </remarks>
        /// <param name="updateRegionDto">UpdateRegionDto object</param>
        /// <returns>Returns RegionViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>        
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegionViewModel>> Update([FromBody] UpdateRegionDto updateRegionDto)
        {
            UpdateRegionCommand command = _mapper.Map<UpdateRegionCommand>(updateRegionDto);
            RegionViewModel region = await Mediator.Send(command);
            return Ok(region);
        }

        /// <summary>
        /// Deletes the region by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /regions/1
        /// </remarks>
        /// <param name="id">Region id (integer)</param>
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
            DeleteRegionCommand command = new DeleteRegionCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
