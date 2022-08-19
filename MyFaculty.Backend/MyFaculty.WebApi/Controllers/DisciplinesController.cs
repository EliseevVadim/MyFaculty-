using AutoMapper;
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
    [Route("api/[controller]")]
    public class DisciplinesController : BaseController
    {
        private IMapper _mapper;

        public DisciplinesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<DisciplinesListViewModel>> GetAll()
        {
            GetDisciplinesQuery query = new GetDisciplinesQuery();
            DisciplinesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DisciplineViewModel>> Get(int id)
        {
            GetDisciplineInfoQuery query = new GetDisciplineInfoQuery()
            {
                Id = id
            };
            DisciplineViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<DisciplineViewModel>> Create([FromBody] CreateDisciplineDto createDisciplineDto)
        {
            CreateDisciplineCommand command = _mapper.Map<CreateDisciplineCommand>(createDisciplineDto);
            DisciplineViewModel discipline = await Mediator.Send(command);
            return Created(nameof(DisciplinesController), discipline);
        }

        [HttpPut]
        public async Task<ActionResult<DisciplineViewModel>> Update([FromBody] UpdateDisciplineDto updateDisciplineDto)
        {
            UpdateDisciplineCommand command = _mapper.Map<UpdateDisciplineCommand>(updateDisciplineDto);
            DisciplineViewModel discipline = await Mediator.Send(command);
            return Ok(discipline);
        }

        [HttpDelete("{id}")]
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
