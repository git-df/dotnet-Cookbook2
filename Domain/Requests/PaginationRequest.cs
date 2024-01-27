using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests
{
    public record PaginationRequest(
        int PageIndex,
        int PageSize);
}
