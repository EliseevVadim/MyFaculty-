using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFaculty.Application.Features.Newsfeed.Queries.GeneratePostsNewsfeedForUser;
using MyFaculty.Application.Features.Newsfeed.Queries.GenerateTasksNewsfeedForUser;
using MyFaculty.Application.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/news")]
    public class NewsfeedController : BaseController
    {
        private readonly IMapper _mapper;

        public NewsfeedController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список записей, отображаемых в новостях текущего пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса: 
        /// GET /news/posts
        /// </remarks>
        /// <returns>Возвращает объект InfoPostsListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("posts")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InfoPostsListViewModel>> GetInformationPosts()
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GeneratePostsNewsfeedForUserQuery query = new GeneratePostsNewsfeedForUserQuery()
            {
                UserId = requesterId
            };
            InfoPostsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Возвращает список заданий, отображаемых в новостях текущего пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:  
        /// GET /news/tasks
        /// </remarks>
        /// <returns>Возвращает объект ClubTasksListViewModel</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="401">Запрос от неавторизованного пользователя</response>
        /// <response code="400">Запрос имеет неверный формат</response>
        [HttpGet("tasks")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClubTasksListViewModel>> GetClubTasks()
        {
            int requesterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GenerateTasksNewsfeedForUserQuery query = new GenerateTasksNewsfeedForUserQuery()
            {
                UserId = requesterId
            };
            ClubTasksListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }
    }
}
