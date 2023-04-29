using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.SecondaryObjectTypes.Commands.CreateSecondaryObjectType;
using MyFaculty.Application.Features.SecondaryObjectTypes.Commands.DeleteSecondaryObjectType;
using MyFaculty.Application.Features.SecondaryObjectTypes.Commands.UpdateSecondaryObjectType;
using MyFaculty.Application.Features.SecondaryObjectTypes.Queries.GetSecondaryObjectTypeInfo;
using MyFaculty.Application.Features.SecondaryObjectTypes.Queries.GetSecondaryObjectTypes;
using MyFaculty.Application.ViewModels;
using MyFaculty.WebApi.Dto;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SecondaryObjectTypesController : BaseController
    {
        private IMapper _mapper;

        public SecondaryObjectTypesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список типов вторичных объектов
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /secondaryobjecttypes
        /// </remarks>
        /// <returns>Возвращает объект SecondaryObjectTypesListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SecondaryObjectTypesListViewModel>> GetAll()
        {
            GetSecondaryObjectTypesQuery query = new GetSecondaryObjectTypesQuery();
            SecondaryObjectTypesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает информацию о типе вторичных объектов по id
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /secondaryobjecttypes/1
        /// </remarks>
        /// <param name="id">id типа вторичных объектов (integer)</param>
        /// <returns>Возвращает объект SecondaryObjectTypeViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Тип вторичных объектов не найден</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SecondaryObjectTypeViewModel>> Get(int id)
        {
            GetSecondaryObjectTypeInfoQuery query = new GetSecondaryObjectTypeInfoQuery()
            {
                Id = id
            };
            SecondaryObjectTypeViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Создает тип вторичных объектов
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /secondaryobjecttypes
        /// {
        ///     "objectTypeName": "string",
        ///     "typePath": "string"
        /// }
        /// </remarks>
        /// <param name="createSecondaryObjectTypeDto">Объект CreateSecondaryObjectTypeDto</param>
        /// <returns>Возвращает объект SecondaryObjectTypeViewModel</returns>
        /// <response code="201">Запись о типе вторичных объектов успешно создана</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        /// <response code="500">Внутренняя серверная ошибка</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SecondaryObjectTypeViewModel>> Create([FromBody] CreateSecondaryObjectTypeDto createSecondaryObjectTypeDto)
        {
            CreateSecondaryObjectTypeCommand command = _mapper.Map<CreateSecondaryObjectTypeCommand>(createSecondaryObjectTypeDto);
            SecondaryObjectTypeViewModel secondaryObjectType = await Mediator.Send(command);
            return Created(nameof(SecondaryObjectTypesController), secondaryObjectType);
        }

        /// <summary>
        /// Редактирует информацию о типе вторичных объектов
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /secondaryobjecttypes
        /// {
        ///     "id": 1,
        ///     "objectTypeName": "string",
        ///     "typePath": "string"
        /// }
        /// </remarks>
        /// <param name="updateSecondaryObjectTypeDto">Объект UpdateSecondaryObjectTypeDto</param>
        /// <returns>Возвращает объект SecondaryObjectTypeViewModel</returns>
        /// <response code="200">Информация о типе вторичных объектов успешно обновлена</response>
        /// <response code="404">>Тип вторичных объектов не найден</response>
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
        public async Task<ActionResult<SecondaryObjectTypeViewModel>> Update([FromBody] UpdateSecondaryObjectTypeDto updateSecondaryObjectTypeDto)
        {
            UpdateSecondaryObjectTypeCommand command = _mapper.Map<UpdateSecondaryObjectTypeCommand>(updateSecondaryObjectTypeDto);
            SecondaryObjectTypeViewModel secondaryObjectType = await Mediator.Send(command);
            return Ok(secondaryObjectType);
        }

        /// <summary>
        /// Удаляет тип вторичных объектов по id
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /secondaryobjecttypes/1
        /// </remarks>
        /// <param name="id">id типа вторичных объектов (integer)</param>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Информация о типе вторичных объектов удалена</response>
        /// <response code="404">Тип вторичных объектов не найден</response>
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
            DeleteSecondaryObjectTypeCommand command = new DeleteSecondaryObjectTypeCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
