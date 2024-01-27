using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StarredCategory : BaseEntity
    {
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
