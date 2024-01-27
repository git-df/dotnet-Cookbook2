using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? Id =>
            Guid.TryParse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out var id) ? id : null;

        public bool IsAuthenticated =>
            _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string Name =>
            _httpContextAccessor.HttpContext.User.Identity.Name;

        public string FirstName =>
            _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "FirstName")?.Value;

        public string LastName =>
            _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "LastName")?.Value;

        public string FullName =>
            _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "FullName")?.Value;

        public bool Blocked =>
            Boolean.TryParse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Blocked")?.Value, out var blocked) && blocked;

        public bool BlockedComments =>
            Boolean.TryParse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "BlockedComments")?.Value, out var blocked) && blocked;
    }
}
