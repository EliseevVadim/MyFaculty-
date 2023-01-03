using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFaculty.Application.Dto;
using MyFaculty.Application.Features.Teachers.Commands.CreateTeacher;
using MyFaculty.Application.Features.Teachers.Commands.DeleteTeacher;
using MyFaculty.Application.Features.Teachers.Commands.UpdateTeacher;
using MyFaculty.Application.Features.Teachers.Commands.VerifyTeacher;
using MyFaculty.Application.Features.Teachers.Queries.GetTeacherInfo;
using MyFaculty.Application.Features.Teachers.Queries.GetTeachers;
using MyFaculty.Application.Features.Teachers.Queries.GetVerificationTokenQuery;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using MyFaculty.WebApi.Dto;
using MyFaculty.WebApi.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyFaculty.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TeachersController : BaseController
    {
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;
        private string _appDomain;

        public TeachersController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _appDomain = configuration["AppDomain"];
        }

        /// <summary>
        /// Gets the list of teachers
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /teachers
        /// </remarks>
        /// <returns>Returns TeachersListViewModel</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TeachersListViewModel>> GetAll()
        {
            GetTeachersQuery query = new GetTeachersQuery();
            TeachersListViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the teacher by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /teachers/1
        /// </remarks>
        /// <param name="id">Teacher's id (integer)</param>
        /// <returns>Returns TeacherViewModel</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeacherViewModel>> Get(int id)
        {
            GetTeacherInfoQuery query = new GetTeacherInfoQuery()
            {
                Id = id
            };
            TeacherViewModel viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        /// <summary>
        /// Sends the teacher's verification token to teacher's email by id
        /// </summary>
        /// <remarks>
        /// Sample request: 
        /// GET /teachers/1/send-verification-token
        /// </remarks>
        /// <param name="id">Teacher's id (integer)</param>
        /// <returns>Returns OkResult</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}/send-verification-token")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SendVerificationToken(int id)
        {
            GetVerificationTokenQuery query = new GetVerificationTokenQuery()
            {
                Id = id
            };
            TeacherVerificationCredentialsDto credentials = await Mediator.Send(query);
            EmailService emailService = new EmailService(_configuration);
            await emailService.SendEmailAsync(credentials.Email, "Токен преподавательской верификации", $"Ваш токен: {credentials.VerificationToken}");
            return Ok();
        }

        /// <summary>
        /// Creates the teacher
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /teachers/verify
        /// {
        ///     "userId": 1,
        ///     "verificationToken": "guid"
        /// }
        /// </remarks>
        /// <param name="verifyTeacherDto">VerifyTeacherDto object</param>
        /// <returns>Retruns OkResult</returns>
        /// <response code="200">Sucess</response>
        /// <response code="403">Forbidden</response> 
        /// <response code="500">Server error</response>
        [HttpPost("verify")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] VerifyTeacherDto verifyTeacherDto)
        {
            VerifyTeacherCommand command = _mapper.Map<VerifyTeacherCommand>(verifyTeacherDto);
            TeacherVerificationDto verificationDto = await Mediator.Send(command);
            if (!verificationDto.VerificationIsSuccessful)
                return Forbid();
            RolesService rolesService = new RolesService();
            await rolesService.AddUserToRoleAsync(verificationDto.VerifyingAcconut.Id, "Teacher");
            return Ok();
        }

        /// <summary>
        /// Creates the teacher
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /teachers
        /// {
        ///     "fio": "string",
        ///     "photo": "file",
        ///     "email": "string",
        ///     "birthDate": "2022-08-27T16:35:47.735Z",
        ///     "scienceRankId": 1
        /// }
        /// </remarks>
        /// <param name="createTeacherDto">CreateTeacherDto object</param>
        /// <returns>Retruns TeacherViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TeacherViewModel>> Create([FromForm] CreateTeacherDto createTeacherDto)
        {
            string photoPath = string.Empty;
            string savePath = string.Empty;
            if (createTeacherDto.Photo != null)
            {
                photoPath = Guid.NewGuid().ToString() + "_" + createTeacherDto.Photo.FileName;
                savePath = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads/images/teachers", photoPath);
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    createTeacherDto.Photo.CopyTo(stream);
                }
            }                    
            CreateTeacherCommand command = _mapper.Map<CreateTeacherCommand>(createTeacherDto);
            command.PhotoPath = String.IsNullOrEmpty(photoPath) ? string.Empty : _appDomain + "uploads/images/teachers/" + photoPath;
            TeacherViewModel teacher = await Mediator.Send(command);
            return Created(nameof(TeachersController), teacher);
        }

        /// <summary>
        /// Updates the teacher
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /teachers
        /// {
        ///     "id": 1,
        ///     "fio": "string",
        ///     "photo": "file",
        ///     "email": "string",
        ///     "birthDate": "2022-08-27T16:35:47.735Z",
        ///     "scienceRankId": 1
        /// }
        /// </remarks>
        /// <param name="updateTeacherDto">UpdateTeacherDto object</param>
        /// <returns>Retruns TeacherViewModel</returns>
        /// <response code="201">Created</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TeacherViewModel>> Update([FromForm] UpdateTeacherDto updateTeacherDto)
        {
            string photoPath = string.Empty;
            string savePath = string.Empty;
            if (updateTeacherDto.Photo != null)
            {
                photoPath = Guid.NewGuid().ToString() + "_" + updateTeacherDto.Photo.FileName;
                savePath = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads/images/teachers", photoPath);
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    updateTeacherDto.Photo.CopyTo(stream);
                }
            }
            UpdateTeacherCommand command = _mapper.Map<UpdateTeacherCommand>(updateTeacherDto);
            command.PhotoPath = String.IsNullOrEmpty(photoPath) ? string.Empty : _appDomain + "uploads/images/teachers/" + photoPath;
            TeacherViewModel teacher = await Mediator.Send(command);
            return Ok(teacher);
        }

        /// <summary>
        /// Deletes the teacher by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /teachers/1
        /// </remarks>
        /// <param name="id">Teacher's id (integer)</param>
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
            DeleteTeacherCommand command = new DeleteTeacherCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
