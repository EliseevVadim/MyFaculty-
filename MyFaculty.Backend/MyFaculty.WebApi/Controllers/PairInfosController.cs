using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.PairInfos.Commands.CreatePairInfo;
using MyFaculty.Application.Features.PairInfos.Commands.DeletePairInfo;
using MyFaculty.Application.Features.PairInfos.Commands.UpdatePairInfo;
using MyFaculty.Application.Features.PairInfos.Queries.GetPairInfoDetails;
using MyFaculty.Application.Features.PairInfos.Queries.GetPairInfos;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PairInfosController : BaseController
    {
        private IMapper _mapper;

        public PairInfosController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PairInfosListViewModel>> GetAll()
        {
            GetPairInfosQuery query = new GetPairInfosQuery();
            PairInfosListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PairInfoViewModel>> Get(int id)
        {
            GetPairInfoDetailsQuery query = new GetPairInfoDetailsQuery()
            {
                Id = id
            };
            PairInfoViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<PairInfo>> Create([FromBody] CreatePairInfoDto createPairInfoDto)
        {
            CreatePairInfoCommand command = _mapper.Map<CreatePairInfoCommand>(createPairInfoDto);
            PairInfo pairInfo = await Mediator.Send(command);
            return Created(nameof(PairInfosController), pairInfo);
        }

        [HttpPut]
        public async Task<ActionResult<PairInfo>> Update([FromBody] UpdatePairInfoDto updatePairInfoDto)
        {
            UpdatePairInfoCommand command = _mapper.Map<UpdatePairInfoCommand>(updatePairInfoDto);
            PairInfo pairInfo = await Mediator.Send(command);
            return Ok(pairInfo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeletePairInfoCommand command = new DeletePairInfoCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
