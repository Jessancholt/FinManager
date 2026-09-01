using FinManager.WebApi.ApiModels.User;
using FluentValidation;

namespace FinManager.WebApi.Validators.User
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(u => u)
                .NotNull();

            RuleFor(u => u.Name)
                .NotEmpty();

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
