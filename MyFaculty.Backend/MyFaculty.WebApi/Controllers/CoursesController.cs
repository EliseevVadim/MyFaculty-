using AutoMapper;
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
    [Route("api/[controller]")]
    public class CoursesController : BaseController
    {
        private IMapper _mapper;

        public CoursesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CoursesListViewModel>> GetAll()
        {
            GetCoursesQuery query = new GetCoursesQuery();
            CoursesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseViewModel>> Get(int id)
        {
            GetCourseInfoQuery query = new GetCourseInfoQuery()
            {
                Id = id
            };
            CourseViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<CourseViewModel>> Create([FromBody] CreateCourseDto createCourseDto)
        {
            CreateCourseCommand command = _mapper.Map<CreateCourseCommand>(createCourseDto);
            CourseViewModel course = await Mediator.Send(command);
            return Created(nameof(CoursesController), course);
        }

        [HttpPut]
        public async Task<ActionResult<CourseViewModel>> Update([FromBody] UpdateCourseDto updateCourseDto)
        {
            UpdateCourseCommand command = _mapper.Map<UpdateCourseCommand>(updateCourseDto);
            CourseViewModel course = await Mediator.Send(command);
            return Ok(course);
        }

        [HttpDelete("{id}")]
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
