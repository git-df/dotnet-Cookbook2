using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Funcions.Category.Commands.AddStarredCategory
{
    public class AddStarredCategoryCommand : IBaseRequest
    {
        public int CategoryId { get; set; }

        public AddStarredCategoryCommand(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
