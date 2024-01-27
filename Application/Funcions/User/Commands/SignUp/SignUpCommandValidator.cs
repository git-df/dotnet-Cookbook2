using Domain.Consts;
using FluentValidation;

namespace Application.Funcions.User.Commands.SignUp
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "email address"))
                .EmailAddress()
                .WithMessage(ValidatorConsts.EmailAddress);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "first name"));

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "last name"));

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "password"))
                .MinimumLength(8)
                .WithMessage(string.Format(ValidatorConsts.MinimumLength, "8"));

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "password confirmation"))
                .Equal(x => x.Password)
                .WithMessage(string.Format(ValidatorConsts.Equal, "password"));
        }
    }
}
