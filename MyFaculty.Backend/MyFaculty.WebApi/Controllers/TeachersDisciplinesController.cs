using AutoMapper;
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
    [Route("api/[controller]")]
    public class TeachersDisciplinesController : BaseController
    {
        private IMapper _mapper;

        public TeachersDisciplinesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TeachersDisciplinesListViewModel>> GetAll()
        {
            GetTeachersDisciplinesQuery query = new GetTeachersDisciplinesQuery();
            TeachersDisciplinesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDisciplineViewModel>> Get(int id)
        {
            GetTeacherDisciplineInfoQuery query = new GetTeacherDisciplineInfoQuery()
            {
                Id = id
            };
            TeacherDisciplineViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<TeacherDisciplineViewModel>> Create([FromBody] CreateTeacherDisciplineDto createTeacherDisciplineDto)
        {
            CreateTeacherDisciplineCommand command = _mapper.Map<CreateTeacherDisciplineCommand>(createTeacherDisciplineDto);
            TeacherDisciplineViewModel teacherDiscipline = await Mediator.Send(command);
            return Created(nameof(TeachersDisciplinesController), teacherDiscipline);
        }

        [HttpPut]
        public async Task<ActionResult<TeacherDisciplineViewModel>> Update([FromBody] UpdateTeacherDisciplineDto updateTeacherDisciplineDto)
        {
            UpdateTeacherDisciplineCommand command = _mapper.Map<UpdateTeacherDisciplineCommand>(updateTeacherDisciplineDto);
            TeacherDisciplineViewModel teacherDiscipline = await Mediator.Send(command);
            return Ok(teacherDiscipline);
        }

        [HttpDelete("{id}")]
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
