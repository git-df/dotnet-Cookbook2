using Application.Common;
using Application.Funcions.Category.Commands.AddStarredCategory;
using Application.Helpers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Consts;
using Domain.Enums;
using Domain.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Funcions.Category.Commands.RemoveStarredCategory
{
    public class RemoveStarredCategoryCommandHandler : BaseHandler<RemoveStarredCategoryCommand>
    {
        public const string CategoryNotExists = "The category does not exist.";
        public const string CategoryIsNotStarres = "The category is not starred.";
        public const string Success = "Category remove from starred.";

        public RemoveStarredCategoryCommandHandler(CookbookDbContext dbContext, IMapper mapper, IUserService userService, ILogger<RemoveStarredCategoryCommand> logger) :
            base(dbContext, mapper, userService, logger)
        {
        }

        public override async Task<BaseResponse> Handle(RemoveStarredCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!_user.IsAuthenticated)
                return new(
                    Success: false,
                    Alert: new(AlertType.Error, AlertMessageConsts.NotAuthenticated));

            var validationResult = await request.Validate(new RemoveStarredCategoryCommandValidator());
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

            if (categoryStarred is null)
                return new(
                    Success: false,
                    Alert: new(AlertType.Warning, CategoryIsNotStarres));

            categoryStarred.Deleted = true;
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
