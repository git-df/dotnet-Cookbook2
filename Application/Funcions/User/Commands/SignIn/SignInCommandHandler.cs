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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Application.Funcions.User.Commands.SignIn
{
    public class SignInCommandHandler : BaseHandler<SignInCommand>
    {
        public const string AccountNotExists = "The account does not exist.";
        public const string AccountBlocked = "The account is blocked.";
        public const string WrongPassword = "Wrong password.";
        public const string Success = "You have been sign in";

        private readonly SignInManager<Domain.Entities.User> _signInManager;
        private readonly UserManager<Domain.Entities.User> _userManager;

        public SignInCommandHandler(CookbookDbContext dbContext, IMapper mapper, IUserService userService, ILogger<SignInCommand> logger,
            SignInManager<Domain.Entities.User> signInManager, UserManager<Domain.Entities.User> userManager) :
            base(dbContext, mapper, userService, logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public override async Task<BaseResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await request.Validate(new SignInCommandValidator());
            if (!validationResult.IsValid)
                return new(
                    Success: false,
                    ValidationResult: validationResult);

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user is null)
                return new(
                    Success: false,
                    Alert: new(AlertType.Warning, AccountNotExists));

            if (user.Blocked)
                return new(
                    Success: false,
                    Alert: new(AlertType.Error, AccountBlocked));

            var signInResult = await _signInManager
                .PasswordSignInAsync(request.Email, request.Password, false, false);

            if (!signInResult.Succeeded)
                return new(
                    Success: false,
                    Alert: new(AlertType.Warning, WrongPassword));

            user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
            var identityUser = await _userManager.FindByEmailAsync(request.Email);
            List<Claim> claims =
            [
                new("FirstName", user.FirstName),
                new("LastName", user.LastName),
                new("FullName", $"{user.FirstName} {user.LastName}"),
                new("Blocked", user.Blocked.ToString()),
                new("BlockedComments", user.BlockedComments.ToString())
            ];

            await _userManager.AddClaimsAsync(identityUser, claims);

            return new(
                Alert: new(AlertType.Success, Success));
        }
    }
}
