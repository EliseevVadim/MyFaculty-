using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.PairRepeatings.Queries.GetPairRepeatings;
using MyFaculty.Application.ViewModels;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PairRepeatingsController : BaseController
    {
        private IMapper _mapper;

        public PairRepeatingsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of pair repeatings
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /pairrepeatings
        /// </remarks>
        /// <returns>Returns PairRepeatingsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PairRepeatingsListViewModel>> GetAll()
        {
            GetPairRepeatingsQuery query = new GetPairRepeatingsQuery();
            PairRepeatingsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }
    }
}
