using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.SecondaryObjects.Commands.DeleteSecondaryObject;
using MyFaculty.Application.Features.Teachers.Commands.CreateTeacher;
using MyFaculty.Application.Features.Teachers.Commands.DeleteTeacher;
using MyFaculty.Application.Features.Teachers.Commands.UpdateTeacher;
using MyFaculty.Application.Features.Teachers.Queries.GetTeacherInfo;
using MyFaculty.Application.Features.Teachers.Queries.GetTeachers;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TeachersController : BaseController
    {
        private IMapper _mapper;

        public TeachersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of teachers
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /teachers
        /// </remarks>
        /// <returns>Returns TeachersListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TeachersListViewModel>> GetAll()
        {
            GetTeachersQuery query = new GetTeachersQuery();
            TeachersListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the teacher by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /teachers/1
        /// </remarks>
        /// <param name="id">Teacher's id (integer)</param>
        /// <returns>Returns TeacherViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherViewModel>> Get(int id)
        {
            GetTeacherInfoQuery query = new GetTeacherInfoQuery()
            {
                Id = id
            };
            TeacherViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the teacher
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /teachers
        /// {
        ///     "fio": "string",
        ///     "photoPath": "string",
        ///     "email": "string",
        ///     "birthDate": "2022-08-27T16:35:47.735Z",
        ///     "scienceRankId": 1
        /// }
        /// </remarks>
        /// <param name="createTeacherDto">CreateTeacherDto object</param>
        /// <returns>Retruns TeacherViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TeacherViewModel>> Create([FromBody] CreateTeacherDto createTeacherDto)
        {
            CreateTeacherCommand command = _mapper.Map<CreateTeacherCommand>(createTeacherDto);
            TeacherViewModel teacher = await Mediator.Send(command);
            return Created(nameof(TeachersController), teacher);
        }

        /// <summary>
        /// Updates the teacher
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /teachers
        /// {
        ///     "id": 1,
        ///     "fio": "string",
        ///     "photoPath": "string",
        ///     "email": "string",
        ///     "birthDate": "2022-08-27T16:35:47.735Z",
        ///     "scienceRankId": 1
        /// }
        /// </remarks>
        /// <param name="updateTeacherDto">UpdateTeacherDto object</param>
        /// <returns>Retruns TeacherViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TeacherViewModel>> Update([FromBody] UpdateTeacherDto updateTeacherDto)
        {
            UpdateTeacherCommand command = _mapper.Map<UpdateTeacherCommand>(updateTeacherDto);
            TeacherViewModel teacher = await Mediator.Send(command);
            return Ok(teacher);
        }

        /// <summary>
        /// Deletes the teacher by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /teachers/1
        /// </remarks>
        /// <param name="id">Teacher's id (integer)</param>
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
            DeleteTeacherCommand command = new DeleteTeacherCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
