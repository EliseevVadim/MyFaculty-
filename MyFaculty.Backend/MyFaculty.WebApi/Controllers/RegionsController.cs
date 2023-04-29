using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Regions.Commands.CreateRegion;
using MyFaculty.Application.Features.Regions.Commands.DeleteRegion;
using MyFaculty.Application.Features.Regions.Commands.UpdateRegion;
using MyFaculty.Application.Features.Regions.Queries.GetRegionInfo;
using MyFaculty.Application.Features.Regions.Queries.GetRegions;
using MyFaculty.Application.Features.Regions.Queries.GetRegionsForCountry;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RegionsController : BaseController
    {
        private IMapper _mapper;

        public RegionsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список регионов
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /regions
        /// </remarks>
        /// <returns>Возвращает объект RegionsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RegionsListViewModel>> GetAll()
        {
            GetRegionsQuery query = new GetRegionsQuery();
            RegionsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информацию о регионе по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /regions/1
        /// </remarks>
        /// <param name="id">id региона (integer)</param>
        /// <returns>Возвращает объект RegionViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Регион не найден</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegionViewModel>> Get(int id)
        {
            GetRegionInfoQuery query = new GetRegionInfoQuery()
            {
                Id = id
            };
            RegionViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список регионов, принадлежащих указанной стране
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /regions/country/1
        /// </remarks>
        /// <param name="id">id страны (integer)</param>
        /// <returns>Возвращает объект RegionsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("country/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegionsListViewModel>> GetByCountryId(int id)
        {
            GetRegionsForCountryQuery query = new GetRegionsForCountryQuery()
            {
                CountryId = id
            };
            RegionsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новую запись о регионе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /regions
        /// {
        ///     "regionName": "string",
        ///     "countryId": 0
        /// }
        /// </remarks>
        /// <param name="createRegionDto">Объект CreateRegionDto</param>
        /// <returns>Возвращает объект RegionViewModel</returns>
        /// <response code="201">Запись о регионе успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegionViewModel>> Create([FromBody] CreateRegionDto createRegionDto)
        {
            CreateRegionCommand command = _mapper.Map<CreateRegionCommand>(createRegionDto);
            RegionViewModel region = await Mediator.Send(command);
            return Created(nameof(RegionsController), region);
        }

        /// <summary>
        /// Редактирует информацию о регионе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /regions
        /// {
        ///     "id": 1,
        ///     "regionName": "string",
        ///     "countryId": 0
        /// }        
        /// </remarks>
        /// <param name="updateRegionDto">Объект UpdateRegionDto</param>
        /// <returns>Возвращает объект RegionViewModel</returns>
        /// <response code="200">Информация о регионе успешно обновлена</response>
        /// <response code="404">Регион не найден</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>  
        /// <response code="500">Внутренняя серверная ошибка</response>    
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegionViewModel>> Update([FromBody] UpdateRegionDto updateRegionDto)
        {
            UpdateRegionCommand command = _mapper.Map<UpdateRegionCommand>(updateRegionDto);
            RegionViewModel region = await Mediator.Send(command);
            return Ok(region);
        }

        /// <summary>
        /// Удаляет информацию о регионе по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /regions/1
        /// </remarks>
        /// <param name="id">id региона (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Информация о регионе успешно удалена</response>
        /// <response code="404">Регион не найден</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteRegionCommand command = new DeleteRegionCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
