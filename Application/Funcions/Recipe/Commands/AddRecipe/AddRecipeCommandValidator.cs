using Domain.Consts;
using Domain.Entities.Configuration;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Funcions.Recipe.Commands.AddRecipe
{
    public class AddRecipeCommandValidator : AbstractValidator<AddRecipeCommand>
    {
        public AddRecipeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "name"))
                .MaximumLength(RecipeConfiguration.NameLength)
                .WithMessage(string.Format(ValidatorConsts.MaximumLength, RecipeConfiguration.NameLength));

            RuleFor(x => x.Short)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "short"))
                .MaximumLength(RecipeConfiguration.ShortLength)
                .WithMessage(string.Format(ValidatorConsts.MaximumLength, RecipeConfiguration.ShortLength));

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "description"))
                .MaximumLength(RecipeConfiguration.DescriptionLength)
                .WithMessage(string.Format(ValidatorConsts.MaximumLength, RecipeConfiguration.DescriptionLength));

            RuleFor(x => x.CookingTime)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "cooking time"));

            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "image url"));

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorConsts.NotEmpty, "category id"));
        }
    }
}
