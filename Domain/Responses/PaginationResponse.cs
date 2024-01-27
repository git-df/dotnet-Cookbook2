using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public record PaginationResponse(
        int IndexCount,
        int PageCount,
        int FirstIndex,
        int LastIndex);
}
