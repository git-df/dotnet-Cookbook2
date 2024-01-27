using Domain.Entities;
using Domain.Entities.Common;
using Domain.Entities.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Seeds;

namespace Persistence.Data
{
    public class CookbookDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public CookbookDbContext(DbContextOptions<CookbookDbContext> options) : base(options)
        {
        }

        override public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<StarredCategory> StarredCategories { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.Modified = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new RateConfiguration());
            builder.ApplyConfiguration(new RecipeConfiguration());
            builder.ApplyConfiguration(new StarredCategoryConfiguration());

            var roles = IdentitySeed.GetRolesSeed();
            var adminUser = IdentitySeed.GetAdminUserSeed();
            var adminRoles = IdentitySeed.GetAdminUserRolesSeed();
            var categories = CategorySeed.GetSeed();

            builder.Entity<IdentityRole<Guid>>().HasData(roles);
            builder.Entity<User>().HasData(adminUser);
            builder.Entity<IdentityUserRole<Guid>>().HasData(adminRoles);
            builder.Entity<Category>().HasData(categories);
        }
    }
}
