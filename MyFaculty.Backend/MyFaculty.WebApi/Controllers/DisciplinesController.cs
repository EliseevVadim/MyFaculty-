using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Disciplines.Commands.CreateDiscipline;
using MyFaculty.Application.Features.Disciplines.Commands.DeleteDiscipline;
using MyFaculty.Application.Features.Disciplines.Commands.UpdateDiscipline;
using MyFaculty.Application.Features.Disciplines.Queries.GetDisciplineInfo;
using MyFaculty.Application.Features.Disciplines.Queries.GetDisciplines;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DisciplinesController : BaseController
    {
        private IMapper _mapper;

        public DisciplinesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of disciplines
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /disciplines
        /// </remarks>
        /// <returns>Returns DisciplinesListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DisciplinesListViewModel>> GetAll()
        {
            GetDisciplinesQuery query = new GetDisciplinesQuery();
            DisciplinesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the discipline by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /disciplines/1
        /// </remarks>
        /// <param name="id">Discipline's id (integer)</param>
        /// <returns>Returns DisciplineViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DisciplineViewModel>> Get(int id)
        {
            GetDisciplineInfoQuery query = new GetDisciplineInfoQuery()
            {
                Id = id
            };
            DisciplineViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the discipline
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /disciplines
        /// {
        ///     "disciplineName": "string"
        /// }
        /// </remarks>
        /// <param name="createDisciplineDto">CreateDisciplineDto object</param>
        /// <returns>Retruns DisciplineViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DisciplineViewModel>> Create([FromBody] CreateDisciplineDto createDisciplineDto)
        {
            CreateDisciplineCommand command = _mapper.Map<CreateDisciplineCommand>(createDisciplineDto);
            DisciplineViewModel discipline = await Mediator.Send(command);
            return Created(nameof(DisciplinesController), discipline);
        }

        /// <summary>
        /// Creates the discipline
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /disciplines
        /// {
        ///     "id": 1,
        ///     "disciplineName": "string"
        /// }
        /// </remarks>
        /// <param name="updateDisciplineDto">UpdateDisciplineDto object</param>
        /// <returns>Retruns DisciplineViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DisciplineViewModel>> Update([FromBody] UpdateDisciplineDto updateDisciplineDto)
        {
            UpdateDisciplineCommand command = _mapper.Map<UpdateDisciplineCommand>(updateDisciplineDto);
            DisciplineViewModel discipline = await Mediator.Send(command);
            return Ok(discipline);
        }

        /// <summary>
        /// Deletes the discipline by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /disciplines/1
        /// </remarks>
        /// <param name="id">Discipline's id (integer)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteDisciplineCommand command = new DeleteDisciplineCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
