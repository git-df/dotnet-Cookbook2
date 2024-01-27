using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class ListResponse<T>
    {
        public List<T> Items { get; set; }
        public PaginationResponse PaginationInfo { get; set; }
    }
}
