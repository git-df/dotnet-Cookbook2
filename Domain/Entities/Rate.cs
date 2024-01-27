
using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Rate : BaseEntity
    {
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public int Value { get; set; }
    }
}
