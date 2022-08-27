using AutoMapper;
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
        private IMapper _mapper;

        public SecondaryObjectsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of secondary objects
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /secondaryobjects
        /// </remarks>
        /// <returns>Returns SecondaryObjectsListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SecondaryObjectsListViewModel>> GetAll()
        {
            GetSecondaryObjectsQuery query = new GetSecondaryObjectsQuery();
            SecondaryObjectsListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the secondary object by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /secondaryobjects/1
        /// </remarks>
        /// <param name="id">Secondary object's id (integer)</param>
        /// <returns>Returns SecondaryObjectViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Creates the secondary object
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /secondaryobjects
        /// {
        ///     "objectName": "string",
        ///     "positionInfo": "json",
        ///     "secondaryObjectTypeId": 1,
        ///     "floorId": 1
        /// }
        /// </remarks>
        /// <param name="createSecondaryObjectDto">CreateSecondaryObjectDto object</param>
        /// <returns>Retruns SecondaryObjectViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SecondaryObjectViewModel>> Create([FromBody] CreateSecondaryObjectDto createSecondaryObjectDto)
        {
            CreateSecondaryObjectCommand command = _mapper.Map<CreateSecondaryObjectCommand>(createSecondaryObjectDto);
            SecondaryObjectViewModel secondaryObject = await Mediator.Send(command);
            return Created(nameof(SecondaryObjectsController), secondaryObject);
        }

        /// <summary>
        /// Updates the secondary object
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /secondaryobjects
        /// {
        ///     "id": 1,
        ///     "objectName": "string",
        ///     "positionInfo": "json",
        ///     "secondaryObjectTypeId": 1,
        ///     "floorId": 1
        /// }
        /// </remarks>
        /// <param name="updateSecondaryObjectDto">UpdateSecondaryObjectDto object</param>
        /// <returns>Retruns SecondaryObjectViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SecondaryObjectViewModel>> Update([FromBody] UpdateSecondaryObjectDto updateSecondaryObjectDto)
        {
            UpdateSecondaryObjectCommand command = _mapper.Map<UpdateSecondaryObjectCommand>(updateSecondaryObjectDto);
            SecondaryObjectViewModel secondaryObject = await Mediator.Send(command);
            return Ok(secondaryObject);
        }

        /// <summary>
        /// Deletes the secondary object by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /secondaryobjects/1
        /// </remarks>
        /// <param name="id">Secondary object's id (integer)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
