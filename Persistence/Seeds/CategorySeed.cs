using Domain.Entities;

namespace Persistence.Seeds
{
    public static class CategorySeed
    {
        public static List<Category> GetSeed() => [
            new() { Id = 1, Name = "Breakfasts", Created = DateTime.Now },
            new() { Id = 2, Name = "Lunches", Created = DateTime.Now },
            new() { Id = 3, Name = "Dinners", Created = DateTime.Now },
            new() { Id = 4, Name = "Desserts", Created = DateTime.Now },
            new() { Id = 5, Name = "Snacks", Created = DateTime.Now },
            new() { Id = 6, Name = "Drinks", Created = DateTime.Now },
            new() { Id = 7, Name = "Vegetarian", Created = DateTime.Now },
            new() { Id = 8, Name = "Vegan", Created = DateTime.Now },
            new() { Id = 9, Name = "Gluten-Free", Created = DateTime.Now },
            new() { Id = 10, Name = "Quick and Easy", Created = DateTime.Now }];
    }
}
