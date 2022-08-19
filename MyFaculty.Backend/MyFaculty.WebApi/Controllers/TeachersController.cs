using AutoMapper;
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
    [Route("api/[controller]")]
    public class TeachersController : BaseController
    {
        private IMapper _mapper;

        public TeachersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TeachersListViewModel>> GetAll()
        {
            GetTeachersQuery query = new GetTeachersQuery();
            TeachersListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherViewModel>> Get(int id)
        {
            GetTeacherInfoQuery query = new GetTeacherInfoQuery()
            {
                Id = id
            };
            TeacherViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<TeacherViewModel>> Create([FromBody] CreateTeacherDto createTeacherDto)
        {
            CreateTeacherCommand command = _mapper.Map<CreateTeacherCommand>(createTeacherDto);
            TeacherViewModel teacher = await Mediator.Send(command);
            return Created(nameof(TeachersController), teacher);
        }

        [HttpPut]
        public async Task<ActionResult<TeacherViewModel>> Update([FromBody] UpdateTeacherDto updateTeacherDto)
        {
            UpdateTeacherCommand command = _mapper.Map<UpdateTeacherCommand>(updateTeacherDto);
            TeacherViewModel teacher = await Mediator.Send(command);
            return Ok(teacher);
        }

        [HttpDelete("{id}")]
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
