using Domain.Consts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Funcions.User.Commands.SignIn
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "email address"));

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "password"));
        }
    }
}
