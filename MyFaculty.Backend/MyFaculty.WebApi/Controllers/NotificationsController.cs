using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Notifications.Commands.DeleteAllNotificationsForUser;
using MyFaculty.Application.Features.Notifications.Queries.GetNotificationsForUser;
using MyFaculty.Application.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NotificationsController : BaseController
    {
        private readonly IMapper _mapper;

        public NotificationsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список уведомлений для текущего пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /notifications
        /// </remarks>
        /// <returns>Возвращает объект NotificationsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NotificationsListViewModel>> GetForRequester()
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GetNotificationsForUserQuery query = new GetNotificationsForUserQuery()
            {
                UserId = requesterId
            };
            NotificationsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Удаляет уведомлении текущего пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:  
        /// DELETE /notifications
        /// </remarks>
        /// <returns>Возвращает пустой ответ</returns>
        /// <response code="204">Уведомления успешно удалены</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpDelete]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteForRequester()
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            DeleteAllNotificationsForUserCommand command = new DeleteAllNotificationsForUserCommand()
            {
                UserId = requesterId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
