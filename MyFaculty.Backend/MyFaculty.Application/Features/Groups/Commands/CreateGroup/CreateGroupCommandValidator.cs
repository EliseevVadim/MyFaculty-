using FluentValidation;

namespace MyFaculty.Application.Features.Groups.Commands.CreateGroup
{
    public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupCommandValidator()
        {
            RuleFor(command => command.GroupName).NotEmpty().MaximumLength(250);
            RuleFor(command => command.CourseId).NotEmpty();
        }
    }
}
