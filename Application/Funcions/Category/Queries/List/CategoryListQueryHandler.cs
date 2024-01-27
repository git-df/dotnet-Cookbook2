using Application.Common;
using Application.Helpers;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities.Configuration;
using Domain.Enums;
using Domain.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace Application.Funcions.Category.Queries.List
{
    public class CategoryListQueryHandler : BaseHandler<CategoryListQuery, List<CategoryListDto>>
    {
        public const string NoCategories = "No categories.";

        public CategoryListQueryHandler(CookbookDbContext dbContext, IMapper mapper, IUserService userService, ILogger<CategoryListQuery> logger) :
            base(dbContext, mapper, userService, logger)
        {
        }

        public override async Task<BaseResponse<List<CategoryListDto>>> Handle(CategoryListQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<List<CategoryListDto>>(
                Data: await _dbContext.Categories
                .Where(x => !x.Deleted)
                .Select(x => new CategoryListDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    RecipesCount = x.Recipes
                        .Count(x => !x.Deleted),
                    Starred = _user.IsAuthenticated ? x.StarredCategories.Any(x => !x.Deleted && x.UserId == _user.Id) : null
                })
                .OrderByDescending(x => x.Starred)
                .ThenByDescending(x => x.RecipesCount)
                .ToListAsync(cancellationToken));

            if (response.Data.Count == 0)
                return new(
                    Data: response.Data,
                    Success: false,
                    Alert: new(AlertType.Warning, NoCategories));

            return response;
        }
    }
}
