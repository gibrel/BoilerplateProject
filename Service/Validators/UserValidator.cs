using FluentValidation;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Service.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Please fill in a username.")
                .NotNull().WithMessage("Please fill in a username.");
        }
    }
}
