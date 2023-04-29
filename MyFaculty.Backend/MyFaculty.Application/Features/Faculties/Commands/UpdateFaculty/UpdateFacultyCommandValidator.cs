using FluentValidation;

namespace MyFaculty.Application.Features.Faculties.Commands.UpdateFaculty
{
    public class UpdateFacultyCommandValidator : AbstractValidator<UpdateFacultyCommand>
    {
        public UpdateFacultyCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.FacultyName).NotEmpty();
        }
    }
}
