using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Cities.Commands.CreateCity;
using MyFaculty.Application.Features.Cities.Commands.DeleteCity;
using MyFaculty.Application.Features.Cities.Commands.UpdateCity;
using MyFaculty.Application.Features.Cities.Queries.GetCities;
using MyFaculty.Application.Features.Cities.Queries.GetCitiesForRegion;
using MyFaculty.Application.Features.Cities.Queries.GetCityInfo;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CitiesController : BaseController
    {
        private readonly IMapper _mapper;

        public CitiesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список городов
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /cities
        /// </remarks>
        /// <returns>Возвращает объект CitiesListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CitiesListViewModel>> GetAll()
        {
            GetCitiesQuery query = new GetCitiesQuery();
            CitiesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информацию о городе по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /cities/1
        /// </remarks>
        /// <param name="id">id города (integer)</param>
        /// <returns>Возвращает объект CityViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Город не найден</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CityViewModel>> Get(int id)
        {
            GetCityInfoQuery query = new GetCityInfoQuery()
            {
                Id = id
            };
            CityViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список городов, принадлежащих указанному региону
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /cities/region/1
        /// </remarks>
        /// <param name="id">id региона (integer)</param>
        /// <returns>Возвращает объект CitiesListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("region/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CitiesListViewModel>> GetByRegionId(int id)
        {
            GetCitiesForRegionQuery query = new GetCitiesForRegionQuery()
            {
                RegionId = id
            };
            CitiesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новую запись о городе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /cities
        /// {
        ///     "cityName": "string",
        ///     "regionId": 0
        /// }
        /// </remarks>
        /// <param name="createCityDto">Объект CreateCityDto</param>
        /// <returns>Возвращает объект CityViewModel</returns>
        /// <response code="201">Запись о городе успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CityViewModel>> Create([FromBody] CreateCityDto createCityDto)
        {
            CreateCityCommand command = _mapper.Map<CreateCityCommand>(createCityDto);
            CityViewModel city = await Mediator.Send(command);
            return Created(nameof(CitiesController), city);
        }

        /// <summary>
        /// Редактирует информацию о городе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /cities
        /// {
        ///     "id": 1,
        ///     "cityName": "string"
        /// }        
        /// </remarks>
        /// <param name="updateCityDto">Объект UpdateCityDto</param>
        /// <returns>Возвращает объект CityViewModel</returns>
        /// <response code="200">Информация о городе успешно обновлена</response>
        /// <response code="404">Город не найден</response>
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
        public async Task<ActionResult<CityViewModel>> Update([FromBody] UpdateCityDto updateCityDto)
        {
            UpdateCityCommand command = _mapper.Map<UpdateCityCommand>(updateCityDto);
            CityViewModel city = await Mediator.Send(command);
            return Ok(city);
        }

        /// <summary>
        /// Удаляет информацию о городе по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /countries/1
        /// </remarks>
        /// <param name="id">id города (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Информация о городе успешно удалена</response>
        /// <response code="404">Город не найден</response>
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
            DeleteCityCommand command = new DeleteCityCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
