using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Countries.Commands.CreateCountry;
using MyFaculty.Application.Features.Countries.Commands.DeleteCountry;
using MyFaculty.Application.Features.Countries.Commands.UpdateCountry;
using MyFaculty.Application.Features.Countries.Queries.GetCountries;
using MyFaculty.Application.Features.Countries.Queries.GetCountryInfo;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CountriesController : BaseController
    {
        private IMapper _mapper;

        public CountriesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список стран
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /countries
        /// </remarks>
        /// <returns>Возвращает объект CountriesListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CountriesListViewModel>> GetAll()
        {
            GetCountriesQuery query = new GetCountriesQuery();
            CountriesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информацию о стране по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /countries/1
        /// </remarks>
        /// <param name="id">id страны (integer)</param>
        /// <returns>Возвращает объект CountryViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Страна не найдена</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CountryViewModel>> Get(int id)
        {
            GetCountryInfoQuery query = new GetCountryInfoQuery()
            {
                Id = id
            };
            CountryViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новую запись о стране
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /countries
        /// {
        ///     "countryName": "string"
        /// }
        /// </remarks>
        /// <param name="createCountryDto">Объект CreateCountryDto</param>
        /// <returns>Возвращает объект CountryViewModel</returns>
        /// <response code="201">Запись о стране успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryViewModel>> Create([FromBody] CreateCountryDto createCountryDto)
        {
            CreateCountryCommand command = _mapper.Map<CreateCountryCommand>(createCountryDto);
            CountryViewModel country = await Mediator.Send(command);
            return Created(nameof(CountriesController), country);
        }

        /// <summary>
        /// Редактирует информацию о стране
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /countries
        /// {
        ///     "id": 1,
        ///     "countryName": "string"
        /// }        
        /// </remarks>
        /// <param name="updateCountryDto">Объект UpdateCountryDto</param>
        /// <returns>Возвращает объект CountryViewModel</returns>
        /// <response code="200">Информация о стране успешно обновлена</response>
        /// <response code="404">Страна не найдена</response>
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
        public async Task<ActionResult<CountryViewModel>> Update([FromBody] UpdateCountryDto updateCountryDto)
        {
            UpdateCountryCommand command = _mapper.Map<UpdateCountryCommand>(updateCountryDto);
            CountryViewModel country = await Mediator.Send(command);
            return Ok(country);
        }

        /// <summary>
        /// Удаляет информацию о стране по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /countries/1
        /// </remarks>
        /// <param name="id">id страны (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Информация о стране успешно удалена</response>
        /// <response code="404">Страна не найдена</response>
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
            DeleteCountryCommand command = new DeleteCountryCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
