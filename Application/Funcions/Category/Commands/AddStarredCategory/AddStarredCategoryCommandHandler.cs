using Application.Common;
using Application.Helpers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Consts;
using Domain.Entities;
using Domain.Enums;
using Domain.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Funcions.Category.Commands.AddStarredCategory
{
    public class AddStarredCategoryCommandHandler : BaseHandler<AddStarredCategoryCommand>
    {
        public const string CategoryNotExists = "The category does not exist.";
        public const string CategoryIsStarres = "The category is starred.";
        public const string Success = "Category has been starred.";

        public AddStarredCategoryCommandHandler(CookbookDbContext dbContext, IMapper mapper, IUserService userService, ILogger<AddStarredCategoryCommand> logger) :
            base(dbContext, mapper, userService, logger)
        {
        }

        public override async Task<BaseResponse> Handle(AddStarredCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!_user.IsAuthenticated)
                return new(
                    Success: false,
                    Alert: new(AlertType.Error, AlertMessageConsts.NotAuthenticated));

            var validationResult = await request.Validate(new AddStarredCategoryCommandValidator());
            if (!validationResult.IsValid)
                return new(
                    Success: false,
                    ValidationResult: validationResult);

            var categoryExist = await _dbContext.Categories
                .AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

            if (!categoryExist)
                return new(
                    Success: false,
                    Alert: new(AlertType.Error, CategoryNotExists));

            var categoryStarred = await _dbContext.StarredCategories
                .FirstOrDefaultAsync(x => x.CategoryId == request.CategoryId && x.UserId == _user.Id, cancellationToken);

            if (categoryStarred is not null && !categoryStarred.Deleted)
                return new(
                    Success: false,
                    Alert: new(AlertType.Warning, CategoryIsStarres));

            if (categoryStarred is not null)
            {
                categoryStarred.Deleted = false;
            }
            else
            {
                StarredCategory newStarredCategory = new()
                {
                    CategoryId = request.CategoryId,
                    UserId = (Guid)_user.Id
                };

                _dbContext.StarredCategories
                    .Add(newStarredCategory);
            }

            var addResult = await _dbContext
                .SaveChangesAsync(cancellationToken);

            if (addResult != 0)
                return new(
                    Alert: new(AlertType.Success, Success));

            return new(
                Success: false,
                Alert: new(AlertType.Error, AlertMessageConsts.UnknownError));
        }
    }
}
