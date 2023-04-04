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
        private IMapper _mapper;

        public NewsfeedController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of information posts for newsfeed
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /news/posts
        /// </remarks>
        /// <returns>Returns InfoPostsListViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response> 
        [HttpGet("posts")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// Gets the list of club tasks for newsfeed
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /news/tasks
        /// </remarks>
        /// <returns>Returns ClubTasksListViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response> 
        [HttpGet("tasks")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
