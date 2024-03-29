﻿
using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Recipe : BaseEntity
    {
        public string Name { get; set; }
        public string Short { get; set; }
        public string Description { get; set; }
        public TimeSpan CookingTime { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
