using FluentValidation;
using FollowingService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FollowingService.Validators
{
    public class FollowValidator : AbstractValidator<Follow>
    {
        public FollowValidator()
        {
            RuleFor(t => t.FollowId).NotNull().NotEmpty().WithMessage("You must enter followId");
            RuleFor(t => t.FollowedId).NotNull().NotEmpty().WithMessage("You must enter followedId");
            RuleFor(t => t.FollowerId).NotNull().NotEmpty().WithMessage("You must enter followerId");
        }
    }
}
