using Application.Common;
using Domain.Consts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Funcions.Category.Commands.RemoveStarredCategory
{
    public class RemoveStarredCategoryCommandValidator : AbstractValidator<RemoveStarredCategoryCommand>
    {
        public RemoveStarredCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "category id"));
        }
    }
}
