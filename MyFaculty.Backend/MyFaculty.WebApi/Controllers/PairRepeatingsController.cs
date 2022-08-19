using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.PairRepeatings.Queries.GetPairRepeatings;
using MyFaculty.Application.ViewModels;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PairRepeatingsController : BaseController
    {
        private IMapper _mapper;

        public PairRepeatingsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PairRepeatingsListViewModel>> GetAll()
        {
            GetPairRepeatingsQuery query = new GetPairRepeatingsQuery();
            PairRepeatingsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }
    }
}
