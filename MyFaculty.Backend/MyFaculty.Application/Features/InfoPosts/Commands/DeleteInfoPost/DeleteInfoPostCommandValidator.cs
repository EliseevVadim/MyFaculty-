using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Commands.DeleteInfoPost
{
    public class DeleteInfoPostCommandValidator : AbstractValidator<DeleteInfoPostCommand>
    {
        public DeleteInfoPostCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
