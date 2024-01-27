using Application.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Funcions.Recipe.Commands.AddRecipe
{
    public class AddRecipeCommand : IBaseRequest<int>
    {
        public string Name { get; set; }
        public string Short { get; set; }
        public string Description { get; set; }
        public TimeSpan CookingTime { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
