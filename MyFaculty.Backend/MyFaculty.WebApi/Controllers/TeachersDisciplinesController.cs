using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.TeachersDisciplines.Commands.CreateTeacherDiscipline;
using MyFaculty.Application.Features.TeachersDisciplines.Commands.DeleteTeacherDiscipline;
using MyFaculty.Application.Features.TeachersDisciplines.Commands.UpdateTeacherDiscipline;
using MyFaculty.Application.Features.TeachersDisciplines.Queries.GetTeacherDisciplineInfo;
using MyFaculty.Application.Features.TeachersDisciplines.Queries.GetTeachersDisciplines;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TeachersDisciplinesController : BaseController
    {
        private IMapper _mapper;

        public TeachersDisciplinesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of teachers disciplines
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /teachersdisciplines
        /// </remarks>
        /// <returns>Returns TeachersDisciplinesListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TeachersDisciplinesListViewModel>> GetAll()
        {
            GetTeachersDisciplinesQuery query = new GetTeachersDisciplinesQuery();
            TeachersDisciplinesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the teacher discipline by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /teachersdisciplines/1
        /// </remarks>
        /// <param name="id">Teacher's discipline id (integer)</param>
        /// <returns>Returns TeacherDisciplineViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherDisciplineViewModel>> Get(int id)
        {
            GetTeacherDisciplineInfoQuery query = new GetTeacherDisciplineInfoQuery()
            {
                Id = id
            };
            TeacherDisciplineViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the teacher's discipline
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /teachersdisciplines
        /// {
        ///     "teacherId": 1,
        ///     "disciplineId": 1
        /// }
        /// </remarks>
        /// <param name="createTeacherDisciplineDto">CreateTeacherDisciplineDto object</param>
        /// <returns>Retruns TeacherDisciplineViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TeacherDisciplineViewModel>> Create([FromBody] CreateTeacherDisciplineDto createTeacherDisciplineDto)
        {
            CreateTeacherDisciplineCommand command = _mapper.Map<CreateTeacherDisciplineCommand>(createTeacherDisciplineDto);
            TeacherDisciplineViewModel teacherDiscipline = await Mediator.Send(command);
            return Created(nameof(TeachersDisciplinesController), teacherDiscipline);
        }

        /// <summary>
        /// Updates the teacher's discipline
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /teachersdisciplines
        /// {
        ///     "id": 1,
        ///     "teacherId": 1,
        ///     "disciplineId": 1
        /// }
        /// </remarks>
        /// <param name="updateTeacherDisciplineDto">UpdateTeacherDisciplineDto object</param>
        /// <returns>Retruns TeacherDisciplineViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TeacherDisciplineViewModel>> Update([FromBody] UpdateTeacherDisciplineDto updateTeacherDisciplineDto)
        {
            UpdateTeacherDisciplineCommand command = _mapper.Map<UpdateTeacherDisciplineCommand>(updateTeacherDisciplineDto);
            TeacherDisciplineViewModel teacherDiscipline = await Mediator.Send(command);
            return Ok(teacherDiscipline);
        }

        /// <summary>
        /// Deletes the teacher's discipline by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /teachersdisciplines/1
        /// </remarks>
        /// <param name="id">Teacher's discipline id (integer)</param>
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
            DeleteTeacherDisciplineCommand command = new DeleteTeacherDisciplineCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
