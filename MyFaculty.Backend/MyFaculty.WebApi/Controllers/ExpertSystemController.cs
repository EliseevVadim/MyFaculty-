using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.ExpertSystem.States.Queries.GetStates;
using MyFaculty.Application.Features.ExpertSystem.StateTransitions.Queries.GetStateTransitions;
using MyFaculty.Application.ViewModels.ExpertSystemViewModels;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ExpertSystemController : BaseController
    {
        [HttpGet("states")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StatesListViewModel>> GetStates()
        {
            GetStatesQuery query = new GetStatesQuery();
            StatesListViewModel states = await Mediator.Send(query);
            return Ok(states);
        }


        [HttpGet("state-transitions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StateTransitionsListViewModel>> GetStateTransitions()
        {
            GetStateTransitionsQuery query = new GetStateTransitionsQuery();
            StateTransitionsListViewModel states = await Mediator.Send(query);
            return Ok(states);
        }
    }
}
