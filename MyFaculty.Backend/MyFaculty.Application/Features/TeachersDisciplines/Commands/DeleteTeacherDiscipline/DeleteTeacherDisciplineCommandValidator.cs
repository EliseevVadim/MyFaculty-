using FluentValidation;

namespace MyFaculty.Application.Features.TeachersDisciplines.Commands.DeleteTeacherDiscipline
{
    public class DeleteTeacherDisciplineCommandValidator : AbstractValidator<DeleteTeacherDisciplineCommand>
    {
        public DeleteTeacherDisciplineCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
