using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Funcions.Category.Commands.RemoveStarredCategory
{
    public class RemoveStarredCategoryCommand : IBaseRequest
    {
        public int CategoryId { get; set; }

        public RemoveStarredCategoryCommand(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
