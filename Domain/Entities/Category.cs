
using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<StarredCategory> StarredCategories { get; set; }
    }
}
