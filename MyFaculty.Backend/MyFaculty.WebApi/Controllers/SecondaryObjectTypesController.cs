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
        /// Gets the list of secondary object types
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /secondaryobjecttypes
        /// </remarks>
        /// <returns>Returns SecondaryObjectTypesListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SecondaryObjectTypesListViewModel>> GetAll()
        {
            GetSecondaryObjectTypesQuery query = new GetSecondaryObjectTypesQuery();
            SecondaryObjectTypesListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the secondary object type by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /secondaryobjecttypes/1
        /// </remarks>
        /// <param name="id">Secondary object type's id (integer)</param>
        /// <returns>Returns SecondaryObjectTypeViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// Creates the secondary object type
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /secondaryobjecttypes
        /// {
        ///     "objectTypeName": "string",
        ///     "typePath": "string"
        /// }
        /// </remarks>
        /// <param name="createSecondaryObjectTypeDto">CreateSecondaryObjectTypeDto object</param>
        /// <returns>Retruns SecondaryObjectTypeViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SecondaryObjectTypeViewModel>> Create([FromBody] CreateSecondaryObjectTypeDto createSecondaryObjectTypeDto)
        {
            CreateSecondaryObjectTypeCommand command = _mapper.Map<CreateSecondaryObjectTypeCommand>(createSecondaryObjectTypeDto);
            SecondaryObjectTypeViewModel secondaryObjectType = await Mediator.Send(command);
            return Created(nameof(SecondaryObjectTypesController), secondaryObjectType);
        }

        /// <summary>
        /// Updates the secondary object type
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /secondaryobjecttypes
        /// {
        ///     "id": 1,
        ///     "objectTypeName": "string",
        ///     "typePath": "string"
        /// }
        /// </remarks>
        /// <param name="updateSecondaryObjectTypeDto">UpdateSecondaryObjectTypeDto object</param>
        /// <returns>Retruns SecondaryObjectTypeViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SecondaryObjectTypeViewModel>> Update([FromBody] UpdateSecondaryObjectTypeDto updateSecondaryObjectTypeDto)
        {
            UpdateSecondaryObjectTypeCommand command = _mapper.Map<UpdateSecondaryObjectTypeCommand>(updateSecondaryObjectTypeDto);
            SecondaryObjectTypeViewModel secondaryObjectType = await Mediator.Send(command);
            return Ok(secondaryObjectType);
        }

        /// <summary>
        /// Deletes the secondary object type by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /secondaryobjecttypes/1
        /// </remarks>
        /// <param name="id">Secondary object type's id (integer)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
