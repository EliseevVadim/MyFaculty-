using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Commands.CreateInfoPost
{
    public class CreateInfoPostCommandValidator : AbstractValidator<CreateInfoPostCommand>
    {
        public CreateInfoPostCommandValidator()
        {
            RuleFor(command => command.PostAttachmentsUid).NotEmpty();
            RuleFor(command => command.AuthorId).NotEmpty();
        }
    }
}
