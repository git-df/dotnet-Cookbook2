using Application.Common;
using Application.Helpers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Responses;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Funcions.Recipe.Commands.AddRecipe
{
    public class AddRecipeCommandHandler : BaseHandler<AddRecipeCommand, int>
    {
        public AddRecipeCommandHandler(CookbookDbContext dbContext, IMapper mapper, IUserService userService, ILogger<AddRecipeCommand> logger) :
            base(dbContext, mapper, userService, logger)
        {
        }

        public override async Task<BaseResponse<int>> Handle(AddRecipeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await request.Validate(new AddRecipeCommandValidator());
            if (!validationResult.IsValid)
                return new(
                    Success: false,
                    ValidationResult: validationResult);

            throw new NotImplementedException();
        }
    }
}
