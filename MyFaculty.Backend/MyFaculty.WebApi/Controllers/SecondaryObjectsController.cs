using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.SecondaryObjects.Commands.CreateSecondaryObject;
using MyFaculty.Application.Features.SecondaryObjects.Commands.DeleteSecondaryObject;
using MyFaculty.Application.Features.SecondaryObjects.Commands.UpdateSecondaryObject;
using MyFaculty.Application.Features.SecondaryObjects.Queries.GetSecondaryObjectInfo;
using MyFaculty.Application.Features.SecondaryObjects.Queries.GetSecondaryObjects;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class SecondaryObjectsController : BaseController
    {
        private IMapper _mapper;

        public SecondaryObjectsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<SecondaryObjectsListViewModel>> GetAll()
        {
            GetSecondaryObjectsQuery query = new GetSecondaryObjectsQuery();
            SecondaryObjectsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SecondaryObjectViewModel>> Get(int id)
        {
            GetSecondaryObjectInfoQuery query = new GetSecondaryObjectInfoQuery()
            {
                Id = id
            };
            SecondaryObjectViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<SecondaryObjectViewModel>> Create([FromBody] CreateSecondaryObjectDto createSecondaryObjectDto)
        {
            CreateSecondaryObjectCommand command = _mapper.Map<CreateSecondaryObjectCommand>(createSecondaryObjectDto);
            SecondaryObjectViewModel secondaryObject = await Mediator.Send(command);
            return Created(nameof(SecondaryObjectsController), secondaryObject);
        }

        [HttpPut]
        public async Task<ActionResult<SecondaryObjectViewModel>> Update([FromBody] UpdateSecondaryObjectDto updateSecondaryObjectDto)
        {
            UpdateSecondaryObjectCommand command = _mapper.Map<UpdateSecondaryObjectCommand>(updateSecondaryObjectDto);
            SecondaryObjectViewModel secondaryObject = await Mediator.Send(command);
            return Ok(secondaryObject);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteSecondaryObjectCommand command = new DeleteSecondaryObjectCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
