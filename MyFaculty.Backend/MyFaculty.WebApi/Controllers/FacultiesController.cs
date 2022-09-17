using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Faculties.Commands.CreateFaculty;
using MyFaculty.Application.Features.Faculties.Commands.DeleteFaculty;
using MyFaculty.Application.Features.Faculties.Commands.UpdateFaculty;
using MyFaculty.Application.Features.Faculties.Queries.GetFaculties;
using MyFaculty.Application.Features.Faculties.Queries.GetFacultyInfo;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FacultiesController : BaseController
    {
        private IMapper _mapper;

        public FacultiesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of faculties
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /faculties
        /// </remarks>
        /// <returns>Returns FacultiesListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<FacultiesListViewModel>> GetAll()
        {
            GetFacultiesQuery query = new GetFacultiesQuery();
            FacultiesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the faculty by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /faculties/1
        /// </remarks>
        /// <param name="id">Faculty id (integer)</param>
        /// <returns>Returns FacultyViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FacultyViewModel>> Get(int id)
        {
            GetFacultyInfoQuery query = new GetFacultyInfoQuery()
            {
                Id = id
            };
            FacultyViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the faculty
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /faculties
        /// {
        ///     "facultyName": "string",
        ///     "officialWebsite": "string"
        /// }
        /// </remarks>
        /// <param name="createFacultyDto">CreateFacultyDto object</param>
        /// <returns>Retruns FacultyViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FacultyViewModel>> Create([FromBody] CreateFacultyDto createFacultyDto)
        {
            CreateFacultyCommand command = _mapper.Map<CreateFacultyCommand>(createFacultyDto);
            FacultyViewModel floor = await Mediator.Send(command);
            return Created(nameof(FacultiesController), floor);
        }

        /// <summary>
        /// Updates the faculty
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /faculties
        /// {
        ///     "id": 1,
        ///     "facultyName": "string",
        ///     "officialWebsite": "string"
        /// }
        /// </remarks>
        /// <param name="updateFacultyDto">UpdateFacultyDto object</param>
        /// <returns>Retruns FacultyViewModel</returns>
        /// <response code="200">Created</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FacultyViewModel>> Update([FromBody] UpdateFacultyDto updateFacultyDto)
        {
            UpdateFacultyCommand command = _mapper.Map<UpdateFacultyCommand>(updateFacultyDto);
            FacultyViewModel floor = await Mediator.Send(command);
            return Ok(floor);
        }

        /// <summary>
        /// Deletes the floor by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /faculties/1
        /// </remarks>
        /// <param name="id">Faculty id (integer)</param>
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
            DeleteFacultyCommand command = new DeleteFacultyCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
