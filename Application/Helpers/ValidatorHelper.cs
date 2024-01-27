using Azure.Core;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class ValidatorHelper
    {
        public async static Task<ValidationResult> Validate<T, TValidator>(this T value, TValidator validator) where TValidator : AbstractValidator<T>
            => await validator.ValidateAsync(value);
    }
}
