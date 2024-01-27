using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        override public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Blocked { get; set; } = false;
        public bool BlockedComments { get; set; } = false;
        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<StarredCategory> StarredCategories { get; set; }
    }
}
