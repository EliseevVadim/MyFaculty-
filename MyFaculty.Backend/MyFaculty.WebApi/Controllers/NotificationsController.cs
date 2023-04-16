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
        private IMapper _mapper;

        public NotificationsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of notifications for requester
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /notifications
        /// </remarks>
        /// <returns>Returns NotificationsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// Deletes all notifications for requester
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// DELETE /notifications
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        [HttpDelete]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
