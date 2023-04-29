using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.AdministrativeReports.Queries.GetInformationPublicsBansReports;
using MyFaculty.Application.Features.AdministrativeReports.Queries.GetUsersBansReports;
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
        /// Возвращает список отчетов о блокировках пользователей
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /administrativereports/usersbans
        /// </remarks>
        /// <returns>Возвращает объект UsersBansReportsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        [HttpGet("usersbans")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UsersBansReportsListViewModel>> GetUsersBansReports()
        {
            GetUsersBansReportsQuery query = new GetUsersBansReportsQuery();
            UsersBansReportsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список отчетов о блокировках информационных сообществ
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /administrativereports/publicsbans
        /// </remarks>
        /// <returns>Возвращает объект InformationPublicsBansReportsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        [HttpGet("publicsbans")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<InformationPublicsBansReportsListViewModel>> GetInformationPublicsBansReports()
        {
            GetInformationPublicsBansReportsQuery query = new GetInformationPublicsBansReportsQuery();
            InformationPublicsBansReportsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }
    }
}
