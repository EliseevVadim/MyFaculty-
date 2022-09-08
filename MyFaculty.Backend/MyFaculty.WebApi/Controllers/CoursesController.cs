using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Courses.Commands.CreateCourse;
using MyFaculty.Application.Features.Courses.Commands.DeleteCourse;
using MyFaculty.Application.Features.Courses.Commands.UpdateCourse;
using MyFaculty.Application.Features.Courses.Queries.GetCourseInfo;
using MyFaculty.Application.Features.Courses.Queries.GetCourses;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CoursesController : BaseController
    {
        private IMapper _mapper;

        public CoursesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of cousres
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /cousres
        /// </remarks>
        /// <returns>Returns CoursesListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CoursesListViewModel>> GetAll()
        {
            GetCoursesQuery query = new GetCoursesQuery();
            CoursesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the course by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /cousres/1
        /// </remarks>
        /// <param name="id">Course's id (integer)</param>
        /// <returns>Returns CourseViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CourseViewModel>> Get(int id)
        {
            GetCourseInfoQuery query = new GetCourseInfoQuery()
            {
                Id = id
            };
            CourseViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Creates the course
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /courses
        /// {
        ///     "courseName": "string",
        ///     "courseNumber": 0
        /// }
        /// </remarks>
        /// <param name="createCourseDto">CreateCourseDto object</param>
        /// <returns>Retruns CourseViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CourseViewModel>> Create([FromBody] CreateCourseDto createCourseDto)
        {
            CreateCourseCommand command = _mapper.Map<CreateCourseCommand>(createCourseDto);
            CourseViewModel course = await Mediator.Send(command);
            return Created(nameof(CoursesController), course);
        }

        /// <summary>
        /// Updates the course
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /courses
        /// {
        ///     "id": 1,
        ///     "courseName": "string",
        ///     "courseNumber": 0
        /// }
        /// </remarks>
        /// <param name="updateCourseDto">UpdateCourseDto object</param>
        /// <returns>Retruns CourseViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CourseViewModel>> Update([FromBody] UpdateCourseDto updateCourseDto)
        {
            UpdateCourseCommand command = _mapper.Map<UpdateCourseCommand>(updateCourseDto);
            CourseViewModel course = await Mediator.Send(command);
            return Ok(course);
        }

        /// <summary>
        /// Deletes the course by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /courses/1
        /// </remarks>
        /// <param name="id">Course's id (integer)</param>
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
            DeleteCourseCommand command = new DeleteCourseCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
