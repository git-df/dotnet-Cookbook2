using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests
{
    public record SortingRequest(
        string OrderBy,
        bool OrderByDescending);
}
