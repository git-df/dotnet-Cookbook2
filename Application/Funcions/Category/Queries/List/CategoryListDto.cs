using Application.Common;
using AutoMapper;

namespace Application.Funcions.Category.Queries.List
{
    public class CategoryListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RecipesCount { get; set; }
        public bool? Starred { get; set; }
    }
}
