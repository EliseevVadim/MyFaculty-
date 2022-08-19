using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Auditoriums.Commands.CreateAuditorium;
using MyFaculty.Application.Features.Auditoriums.Commands.DeleteAuditorium;
using MyFaculty.Application.Features.Auditoriums.Commands.UpdateAuditorium;
using MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriumInfo;
using MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriums;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuditoriumsController : BaseController
    {
        private IMapper _mapper;

        public AuditoriumsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<AuditoriumsListViewModel>> GetAll()
        {
            GetAuditoriumsQuery query = new GetAuditoriumsQuery();
            AuditoriumsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuditoriumViewModel>> Get(int id)
        {
            GetAuditoriumInfoQuery query = new GetAuditoriumInfoQuery()
            {
                Id = id
            };
            AuditoriumViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<AuditoriumViewModel>> Create([FromBody] CreateAuditoriumDto createAuditoriumDto)
        {
            CreateAuditoriumCommand command = _mapper.Map<CreateAuditoriumCommand>(createAuditoriumDto);
            AuditoriumViewModel auditorium = await Mediator.Send(command);
            return Created(nameof(AuditoriumsController), auditorium);
        }

        [HttpPut]
        public async Task<ActionResult<AuditoriumViewModel>> Update([FromBody] UpdateAuditoriumDto updateAuditoriumDto)
        {
            UpdateAuditoriumCommand command = _mapper.Map<UpdateAuditoriumCommand>(updateAuditoriumDto);
            AuditoriumViewModel course = await Mediator.Send(command);
            return Ok(course);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteAuditoriumCommand command = new DeleteAuditoriumCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
