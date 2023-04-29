using FluentValidation;

namespace MyFaculty.Application.Features.Groups.Commands.UpdateGroup
{
    public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.GroupName).NotEmpty().MaximumLength(250);
            RuleFor(command => command.CourseId).NotEmpty();
        }
    }
}
