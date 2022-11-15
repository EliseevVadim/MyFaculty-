using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Cities.Commands.CreateCity;
using MyFaculty.Application.Features.Cities.Commands.DeleteCity;
using MyFaculty.Application.Features.Cities.Commands.UpdateCity;
using MyFaculty.Application.Features.Cities.Queries.GetCities;
using MyFaculty.Application.Features.Cities.Queries.GetCitiesForRegion;
using MyFaculty.Application.Features.Cities.Queries.GetCityInfo;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CitiesController : BaseController
    {
        private IMapper _mapper;

        public CitiesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of cities
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /cities
        /// </remarks>
        /// <returns>Returns CitiesListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CitiesListViewModel>> GetAll()
        {
            GetCitiesQuery query = new GetCitiesQuery();
            CitiesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the city by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /cities/1
        /// </remarks>
        /// <param name="id">City id (integer)</param>
        /// <returns>Returns CityViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CityViewModel>> Get(int id)
        {
            GetCityInfoQuery query = new GetCityInfoQuery()
            {
                Id = id
            };
            CityViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the list of cities for a specific region
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /cities/region/1
        /// </remarks>
        /// <param name="id">Specific region id (integer)</param>
        /// <returns>Returns CitiesListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet("region/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CitiesListViewModel>> GetByRegionId(int id)
        {
            GetCitiesForRegionQuery query = new GetCitiesForRegionQuery()
            {
                RegionId = id
            };
            CitiesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the city
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /cities
        /// {
        ///     "cityName": "string",
        ///     "regionId": 0
        /// }
        /// </remarks>
        /// <param name="createCityDto">CreateCityDto object</param>
        /// <returns>Retruns CityViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CityViewModel>> Create([FromBody] CreateCityDto createCityDto)
        {
            CreateCityCommand command = _mapper.Map<CreateCityCommand>(createCityDto);
            CityViewModel city = await Mediator.Send(command);
            return Created(nameof(CitiesController), city);
        }

        /// <summary>
        /// Updates the city
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /cities
        /// {
        ///     "id": 1,
        ///     "cityName": "string"
        /// }        
        /// </remarks>
        /// <param name="updateCityDto">UpdateCityDto object</param>
        /// <returns>Returns CityViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>        
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CityViewModel>> Update([FromBody] UpdateCityDto updateCityDto)
        {
            UpdateCityCommand command = _mapper.Map<UpdateCityCommand>(updateCityDto);
            CityViewModel city = await Mediator.Send(command);
            return Ok(city);
        }

        /// <summary>
        /// Deletes the city by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /countries/1
        /// </remarks>
        /// <param name="id">City id (integer)</param>
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
            DeleteCityCommand command = new DeleteCityCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
