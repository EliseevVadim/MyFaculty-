using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Commands.LikeInfoPost
{
    public class LikeInfoPostCommandValidator : AbstractValidator<LikeInfoPostCommand>
    {
        public LikeInfoPostCommandValidator()
        {
            RuleFor(command => command.LikedPostId).NotEmpty();
            RuleFor(command => command.LikedUserId).NotEmpty();
        }
    }
}
