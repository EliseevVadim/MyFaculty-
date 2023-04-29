using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.SecondaryObjects.Commands.CreateSecondaryObject;
using MyFaculty.Application.Features.SecondaryObjects.Commands.DeleteSecondaryObject;
using MyFaculty.Application.Features.SecondaryObjects.Commands.UpdateSecondaryObject;
using MyFaculty.Application.Features.SecondaryObjects.Queries.GetSecondaryObjectInfo;
using MyFaculty.Application.Features.SecondaryObjects.Queries.GetSecondaryObjects;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SecondaryObjectsController : BaseController
    {
        private readonly IMapper _mapper;

        public SecondaryObjectsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список вторичных объектов
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /secondaryobjects
        /// </remarks>
        /// <returns>Возвращает объект SecondaryObjectsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SecondaryObjectsListViewModel>> GetAll()
        {
            GetSecondaryObjectsQuery query = new GetSecondaryObjectsQuery();
            SecondaryObjectsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информацию о вторичном объекте по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /secondaryobjects/1
        /// </remarks>
        /// <param name="id">id вторичного объекта (integer)</param>
        /// <returns>Возвращает объект SecondaryObjectViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Вторичный объект не найден</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SecondaryObjectViewModel>> Get(int id)
        {
            GetSecondaryObjectInfoQuery query = new GetSecondaryObjectInfoQuery()
            {
                Id = id
            };
            SecondaryObjectViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает новую запись о вторичном объекте
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /secondaryobjects
        /// {
        ///     "objectName": "string",
        ///     "positionInfo": "json",
        ///     "secondaryObjectTypeId": 1,
        ///     "floorId": 1
        /// }
        /// </remarks>
        /// <param name="createSecondaryObjectDto">Объект CreateSecondaryObjectDto</param>
        /// <returns>Возвращает объект SecondaryObjectViewModel</returns>
        /// <response code="201">Запись о вторичном объекте успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SecondaryObjectViewModel>> Create([FromBody] CreateSecondaryObjectDto createSecondaryObjectDto)
        {
            CreateSecondaryObjectCommand command = _mapper.Map<CreateSecondaryObjectCommand>(createSecondaryObjectDto);
            SecondaryObjectViewModel secondaryObject = await Mediator.Send(command);
            return Created(nameof(SecondaryObjectsController), secondaryObject);
        }

        /// <summary>
        /// Редактирует информацию о вторичном объекте
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /secondaryobjects
        /// {
        ///     "id": 1,
        ///     "objectName": "string",
        ///     "positionInfo": "json",
        ///     "secondaryObjectTypeId": 1,
        ///     "floorId": 1
        /// }
        /// </remarks>
        /// <param name="updateSecondaryObjectDto">Объект UpdateSecondaryObjectDto</param>
        /// <returns>Возвращает объект SecondaryObjectViewModel</returns>
        /// <response code="200">Информация о вторичном объекте успешно обновлена</response>
        /// <response code="404">Вторичный объект не найден</response>
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
        public async Task<ActionResult<SecondaryObjectViewModel>> Update([FromBody] UpdateSecondaryObjectDto updateSecondaryObjectDto)
        {
            UpdateSecondaryObjectCommand command = _mapper.Map<UpdateSecondaryObjectCommand>(updateSecondaryObjectDto);
            SecondaryObjectViewModel secondaryObject = await Mediator.Send(command);
            return Ok(secondaryObject);
        }

        /// <summary>
        /// Удаляет информацию о вторичном объекте по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /secondaryobjects/1
        /// </remarks>
        /// <param name="id">id вторичного объекта (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Информация о вторичном объекте удалена</response>
        /// <response code="404">Вторичный объект не найден</response>
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
            DeleteSecondaryObjectCommand command = new DeleteSecondaryObjectCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
