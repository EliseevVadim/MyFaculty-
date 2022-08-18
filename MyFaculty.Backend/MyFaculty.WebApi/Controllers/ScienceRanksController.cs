using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.ScienceRanks.Commands.CreateScienceRank;
using MyFaculty.Application.Features.ScienceRanks.Commands.DeleteScienceRank;
using MyFaculty.Application.Features.ScienceRanks.Commands.UpdateScienceRank;
using MyFaculty.Application.Features.ScienceRanks.Queries.GetScienceRankInfo;
using MyFaculty.Application.Features.ScienceRanks.Queries.GetScienceRanks;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ScienceRanksController : BaseController
    {
        private IMapper _mapper;

        public ScienceRanksController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ScienceRanksListViewModel>> GetAll()
        {
            GetScienceRanksQuery query = new GetScienceRanksQuery();
            ScienceRanksListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScienceRankViewModel>> Get(int id)
        {
            GetScienceRankInfoQuery query = new GetScienceRankInfoQuery()
            {
                Id = id
            };
            ScienceRankViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult<ScienceRank>> Create([FromBody] CreateScienceRankDto createScienceRankDto)
        {
            CreateScienceRankCommand command = _mapper.Map<CreateScienceRankCommand>(createScienceRankDto);
            ScienceRank rank = await Mediator.Send(command);
            return Created(nameof(ScienceRanksController), rank);
        }

        [HttpPut]
        public async Task<ActionResult<ScienceRank>> Update([FromBody] UpdateScienceRankDto updateScienceRankDto)
        {
            UpdateScienceRankCommand command = _mapper.Map<UpdateScienceRankCommand>(updateScienceRankDto);
            ScienceRank rank = await Mediator.Send(command);
            return Ok(rank);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteScienceRankCommand command = new DeleteScienceRankCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
