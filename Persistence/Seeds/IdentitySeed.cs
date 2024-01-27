using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Seeds
{
    public static class IdentitySeed
    {
        public static List<IdentityRole<Guid>> GetRolesSeed()
        {
            List<IdentityRole<Guid>> roles =
            [
                new() { Id = Guid.Parse("f6f6c492-1aa4-46d6-80b9-3225f3953931"), Name = "user" },
                new() { Id = Guid.Parse("86faf7ea-e31e-4c57-8207-8bfd21214318"), Name = "creator" },
                new() { Id = Guid.Parse("606eb50a-32d2-4e7a-8378-3cdb1905c447"), Name = "admin" }
            ];

            foreach (var role in roles)
            {
                role.ConcurrencyStamp = role.Id.ToString();
                role.NormalizedName = role.Name.ToUpper();
            }

            return roles;
        }

        public static User GetAdminUserSeed()
        {
            User adminUser = new()
            {
                Id = Guid.Parse("a2eea53e-a50e-4c60-98b4-1fcafab821ed"),
                UserName = "sa",
                FirstName = "sa",
                LastName = "sa",
                Email = "sa@sa.sa",
                Blocked = false,
                BlockedComments = false,
                NormalizedEmail = "SA@SA.SA",
                NormalizedUserName = "SA@SA.SA",
            };

            adminUser.PasswordHash = new PasswordHasher<User>()
                .HashPassword(adminUser, "Haslo123!");

            return adminUser;
        }

        public static List<IdentityUserRole<Guid>> GetAdminUserRolesSeed()
        {
            var roles = GetRolesSeed();
            var adminUser = GetAdminUserSeed();

            return roles.Select(role => new IdentityUserRole<Guid>
            {
                RoleId = role.Id,
                UserId = adminUser.Id
            }).ToList();
        }
    }
}
