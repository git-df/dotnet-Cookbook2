using Application.Common;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Consts;
using Domain.Enums;
using Domain.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Funcions.User.Commands.SignOut
{
    public class SignOutCommandHandler : BaseHandler<SignOutCommand>
    {
        public const string Success = "Signed out.";

        private readonly SignInManager<Domain.Entities.User> _signInManager;

        public SignOutCommandHandler(CookbookDbContext dbContext, IMapper mapper, IUserService userService, ILogger<SignOutCommand> logger,
            SignInManager<Domain.Entities.User> signInManager) :
            base(dbContext, mapper, userService, logger)
        {
            _signInManager = signInManager;
        }

        public override async Task<BaseResponse> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            return new(
                Alert: new(AlertType.Success, Success));
        }
    }
}
