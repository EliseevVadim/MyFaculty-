using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.PairInfos.Commands.CreatePairInfo;
using MyFaculty.Application.Features.PairInfos.Commands.DeletePairInfo;
using MyFaculty.Application.Features.PairInfos.Commands.UpdatePairInfo;
using MyFaculty.Application.Features.PairInfos.Queries.GetPairInfoDetails;
using MyFaculty.Application.Features.PairInfos.Queries.GetPairInfos;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PairInfosController : BaseController
    {
        private IMapper _mapper;

        public PairInfosController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список записей метаданных о парах
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /pairinfos
        /// </remarks>
        /// <returns>Возвращает объект PairInfosListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PairInfosListViewModel>> GetAll()
        {
            GetPairInfosQuery query = new GetPairInfosQuery();
            PairInfosListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает запись метаданных о парах по id
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /pairinfos/1
        /// </remarks>
        /// <param name="id">id записи метаданных о парах (integer)</param>
        /// <returns>Возвращает объект PairInfoViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Запись метаданных о парах не найдена</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PairInfoViewModel>> Get(int id)
        {
            GetPairInfoDetailsQuery query = new GetPairInfoDetailsQuery()
            {
                Id = id
            };
            PairInfoViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает запись метаданных о парах
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// POST /pairinfos
        /// {
        ///     "pairNumber": 1,
        ///     "startTime": "string",
        ///     "endTime": "string"
        /// }
        /// </remarks>
        /// <param name="createPairInfoDto">Объект CreatePairInfoDto</param>
        /// <returns>Возвращает объект PairInfoViewModel</returns>
        /// <response code="201">Запись метаданных о парах успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PairInfoViewModel>> Create([FromBody] CreatePairInfoDto createPairInfoDto)
        {
            CreatePairInfoCommand command = _mapper.Map<CreatePairInfoCommand>(createPairInfoDto);
            PairInfoViewModel pairInfo = await Mediator.Send(command);
            return Created(nameof(PairInfosController), pairInfo);
        }

        /// <summary>
        /// Редактирует запись метаданных о парах
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /pairinfos
        /// {
        ///     "id": 1,
        ///     "pairNumber": 1,
        ///     "startTime": "string",
        ///     "endTime": "string"
        /// }
        /// </remarks>
        /// <param name="updatePairInfoDto">Объект UpdatePairInfoDto</param>
        /// <returns>Возвращает объект PairInfoViewModel</returns>
        /// <response code="200">Запись метаданных о парах успешно обновлена</response>
        /// <response code="404">Запись метаданных о парах не найдена</response>
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
        public async Task<ActionResult<PairInfoViewModel>> Update([FromBody] UpdatePairInfoDto updatePairInfoDto)
        {
            UpdatePairInfoCommand command = _mapper.Map<UpdatePairInfoCommand>(updatePairInfoDto);
            PairInfoViewModel pairInfo = await Mediator.Send(command);
            return Ok(pairInfo);
        }

        /// <summary>
        /// Удаляет запись метаданных о парах
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /pairinfos/1
        /// </remarks>
        /// <param name="id">id записи метаданных о парах (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Запись метаданных о парах успешно удалена</response>
        /// <response code="404">Запись метаданных о парах не найдена</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response> 
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            DeletePairInfoCommand command = new DeletePairInfoCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
