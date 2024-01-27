using Application.Common;
using Application.Helpers;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Consts;
using Domain.Enums;
using Domain.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace Application.Funcions.User.Commands.SignUp
{
    public class SignUpCommandHandler : BaseHandler<SignUpCommand>
    {
        public const string Success = "A new user has been registered.";
        public const string EmailExists = "An account with such an email already exists.";

        private readonly UserManager<Domain.Entities.User> _userManager;

        public SignUpCommandHandler(CookbookDbContext dbContext, IMapper mapper, IUserService userService, ILogger<SignUpCommand> logger,
            UserManager<Domain.Entities.User> userManager) :
            base(dbContext, mapper, userService, logger)
        {
            _userManager = userManager;
        }

        public override async Task<BaseResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await request.Validate(new SignUpCommandValidator());
            if (!validationResult.IsValid)
                return new(
                    Success: false,
                    ValidationResult: validationResult);

            var emailExists = await _dbContext.Users
                .AnyAsync(x => x.Email == request.Email.ToLower(), cancellationToken);

            if (emailExists)
                return new(
                    Success: false,
                    Alert: new(AlertType.Warning, EmailExists));

            Domain.Entities.User newUser = new()
            {
                UserName = request.Email.ToLower(),
                FirstName = request.FirstName.ToLower(),
                LastName = request.LastName.ToLower(),
                Email = request.Email.ToLower(),
                Blocked = false,
                BlockedComments = false
            };

            var signUpResult = await _userManager
                .CreateAsync(newUser, request.Password);

            if (!signUpResult.Succeeded)
                return new(
                    Success: false,
                    Alert: new(AlertType.Error, AlertMessageConsts.UnknownError));

            var addRoleResult = await _userManager
                .AddToRoleAsync(newUser, UserRoleConsts.User);

            if (!addRoleResult.Succeeded)
                return new(
                    Success: false,
                    Alert: new(AlertType.Error, AlertMessageConsts.UnknownError));

            return new(
                Alert: new AlertResponse(AlertType.Success, Success));
        }
    }
}
