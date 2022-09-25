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
    public class ExpertSystemController : BaseController
    {
        /// <summary>
        /// Gets the list of states of expert system
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /expertsystem/states
        /// </remarks>
        /// <returns>Returns StatesListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet("states")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StatesListViewModel>> GetStates()
        {
            GetStatesQuery query = new GetStatesQuery();
            StatesListViewModel states = await Mediator.Send(query);
            return Ok(states);
        }

        /// <summary>
        /// Gets the list of state transitions of expert system
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /expertsystem/state-transitions
        /// </remarks>
        /// <returns>Returns StateTransitionsListViewModel</returns>
        /// <response code="200">Success</response>
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
