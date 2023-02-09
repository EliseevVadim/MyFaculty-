using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Commands.UnlikeInfoPost
{
    public class UnlikeInfoPostCommandValidator : AbstractValidator<UnlikeInfoPostCommand>
    {
        public UnlikeInfoPostCommandValidator()
        {
            RuleFor(command => command.LikedPostId).NotEmpty();
            RuleFor(command => command.LikedUserId).NotEmpty();
        }
    }
}
