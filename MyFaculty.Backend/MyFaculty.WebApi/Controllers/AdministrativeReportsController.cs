using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.UsersBansReports.Queries.GetUsersBansReports;
using MyFaculty.Application.ViewModels;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AdministrativeReportsController : BaseController
    {
        private IMapper _mapper;

        public AdministrativeReportsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of users bans reports
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /administrativereports/usersbans
        /// </remarks>
        /// <returns>Returns UsersBansReportsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet("usersbans")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UsersBansReportsListViewModel>> GetUsersBansReports()
        {
            GetUsersBansReportsQuery query = new GetUsersBansReportsQuery();
            UsersBansReportsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }
    }
}
