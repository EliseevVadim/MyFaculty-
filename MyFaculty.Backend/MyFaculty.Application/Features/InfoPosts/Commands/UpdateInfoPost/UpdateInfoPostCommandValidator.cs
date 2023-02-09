using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Commands.UpdateInfoPost
{
    public class UpdateInfoPostCommandValidator : AbstractValidator<UpdateInfoPostCommand>
    {
        public UpdateInfoPostCommandValidator()
        {
            RuleFor(command => command.InfoPostId).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
