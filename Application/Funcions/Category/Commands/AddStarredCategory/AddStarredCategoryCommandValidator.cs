using Domain.Consts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Funcions.Category.Commands.AddStarredCategory
{
    public class AddStarredCategoryCommandValidator : AbstractValidator<AddStarredCategoryCommand>
    {
        public AddStarredCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "category id"));
        }
    }
}
