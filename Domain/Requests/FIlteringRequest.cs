using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests
{
    public record FIlteringRequest(
        string PropertyName,
        FilterType Type,
        object Value = null,
        List<object> Values = null,
        object MinValue = null,
        object MaxValue = null);
}
