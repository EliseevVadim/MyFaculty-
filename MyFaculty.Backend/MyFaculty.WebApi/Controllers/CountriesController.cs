using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Countries.Commands.CreateCountry;
using MyFaculty.Application.Features.Countries.Commands.DeleteCountry;
using MyFaculty.Application.Features.Countries.Commands.UpdateCountry;
using MyFaculty.Application.Features.Countries.Queries.GetCountries;
using MyFaculty.Application.Features.Countries.Queries.GetCountryInfo;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CountriesController : BaseController
    {
        private IMapper _mapper;

        public CountriesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of countries
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /countries
        /// </remarks>
        /// <returns>Returns CountriesListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CountriesListViewModel>> GetAll()
        {
            GetCountriesQuery query = new GetCountriesQuery();
            CountriesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the country by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /countries/1
        /// </remarks>
        /// <param name="id">Country id (integer)</param>
        /// <returns>Returns CountryViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CountryViewModel>> Get(int id)
        {
            GetCountryInfoQuery query = new GetCountryInfoQuery()
            {
                Id = id
            };
            CountryViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the country
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /countries
        /// {
        ///     "countryName": "string"
        /// }
        /// </remarks>
        /// <param name="createCountryDto">CreateCountryDto object</param>
        /// <returns>Retruns CountryViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryViewModel>> Create([FromBody] CreateCountryDto createCountryDto)
        {
            CreateCountryCommand command = _mapper.Map<CreateCountryCommand>(createCountryDto);
            CountryViewModel country = await Mediator.Send(command);
            return Created(nameof(CountriesController), country);
        }

        /// <summary>
        /// Updates the country
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /countries
        /// {
        ///     "id": 1,
        ///     "countryName": "string"
        /// }        
        /// </remarks>
        /// <param name="updateCountryDto">UpdateCountryDto object</param>
        /// <returns>Returns CountryViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>        
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryViewModel>> Update([FromBody] UpdateCountryDto updateCountryDto)
        {
            UpdateCountryCommand command = _mapper.Map<UpdateCountryCommand>(updateCountryDto);
            CountryViewModel country = await Mediator.Send(command);
            return Ok(country);
        }

        /// <summary>
        /// Deletes the country by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /countries/1
        /// </remarks>
        /// <param name="id">Country id (integer)</param>
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
            DeleteCountryCommand command = new DeleteCountryCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
