using Application.Common;

namespace Application.Funcions.User.Commands.SignIn
{
    public class SignInCommand : IBaseRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
