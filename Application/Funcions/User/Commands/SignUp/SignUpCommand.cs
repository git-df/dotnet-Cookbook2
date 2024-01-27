using Application.Common;

namespace Application.Funcions.User.Commands.SignUp
{
    public class SignUpCommand : IBaseRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
