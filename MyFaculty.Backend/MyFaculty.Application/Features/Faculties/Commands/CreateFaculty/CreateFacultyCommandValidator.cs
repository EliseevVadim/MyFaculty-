using FluentValidation;

namespace MyFaculty.Application.Features.Faculties.Commands.CreateFaculty
{
    public class CreateFacultyCommandValidator : AbstractValidator<CreateFacultyCommand>
    {
        public CreateFacultyCommandValidator()
        {
            RuleFor(command => command.FacultyName).NotEmpty();
        }
    }
}
