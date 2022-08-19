using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.SecondaryObjectTypes.Commands.CreateSecondaryObjectType;
using MyFaculty.Application.Features.SecondaryObjectTypes.Commands.DeleteSecondaryObjectType;
using MyFaculty.Application.Features.SecondaryObjectTypes.Commands.UpdateSecondaryObjectType;
using MyFaculty.Application.Features.SecondaryObjectTypes.Queries.GetSecondaryObjectTypeInfo;
using MyFaculty.Application.Features.SecondaryObjectTypes.Queries.GetSecondaryObjectTypes;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class SecondaryObjectTypesController : BaseController
    {
        private IMapper _mapper;

        public SecondaryObjectTypesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<SecondaryObjectTypesListViewModel>> GetAll()
        {
            GetSecondaryObjectTypesQuery query = new GetSecondaryObjectTypesQuery();
            SecondaryObjectTypesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SecondaryObjectTypeViewModel>> Get(int id)
        {
            GetSecondaryObjectTypeInfoQuery query = new GetSecondaryObjectTypeInfoQuery()
            {
                Id = id
            };
            SecondaryObjectTypeViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<SecondaryObjectTypeViewModel>> Create([FromBody] CreateSecondaryObjectTypeDto createSecondaryObjectTypeDto)
        {
            CreateSecondaryObjectTypeCommand command = _mapper.Map<CreateSecondaryObjectTypeCommand>(createSecondaryObjectTypeDto);
            SecondaryObjectTypeViewModel secondaryObjectType = await Mediator.Send(command);
            return Created(nameof(SecondaryObjectTypesController), secondaryObjectType);
        }

        [HttpPut]
        public async Task<ActionResult<SecondaryObjectTypeViewModel>> Update([FromBody] UpdateSecondaryObjectTypeDto updateSecondaryObjectTypeDto)
        {
            UpdateSecondaryObjectTypeCommand command = _mapper.Map<UpdateSecondaryObjectTypeCommand>(updateSecondaryObjectTypeDto);
            SecondaryObjectTypeViewModel secondaryObjectType = await Mediator.Send(command);
            return Ok(secondaryObjectType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteSecondaryObjectTypeCommand command = new DeleteSecondaryObjectTypeCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
