using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public record BaseResponse(
        bool Success = true,
        AlertResponse Alert = null,
        ValidationResult ValidationResult = null);

    public record BaseResponse<T>(
        T Data = default,
        bool Success = true,
        AlertResponse Alert = null,
        ValidationResult ValidationResult = null) : BaseResponse(
            Success,
            Alert,
            ValidationResult);
}
