using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Pairs.Commands.CreatePair;
using MyFaculty.Application.Features.Pairs.Commands.DeletePair;
using MyFaculty.Application.Features.Pairs.Commands.UpdatePair;
using MyFaculty.Application.Features.Pairs.Queries.GetPairInfo;
using MyFaculty.Application.Features.Pairs.Queries.GetPairs;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PairsController : BaseController
    {
        private IMapper _mapper;

        public PairsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PairsListViewModel>> GetAll()
        {
            GetPairsQuery query = new GetPairsQuery();
            PairsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PairViewModel>> Get(int id)
        {
            GetPairInfoQuery query = new GetPairInfoQuery()
            {
                Id = id
            };
            PairViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<PairViewModel>> Create([FromBody] CreatePairDto createPairDto)
        {
            CreatePairCommand command = _mapper.Map<CreatePairCommand>(createPairDto);
            PairViewModel pair = await Mediator.Send(command);
            return Created(nameof(PairsController), pair);
        }

        [HttpPut]
        public async Task<ActionResult<PairViewModel>> Update([FromBody] UpdateCourseDto updateCourseDto)
        {
            UpdatePairCommand command = _mapper.Map<UpdatePairCommand>(updateCourseDto);
            PairViewModel pair = await Mediator.Send(command);
            return Ok(pair);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeletePairCommand command = new DeletePairCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
