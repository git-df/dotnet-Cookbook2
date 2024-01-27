using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        public Guid? Id { get; }
        public bool IsAuthenticated { get; }
        public string Name { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName { get; }
        public bool Blocked { get; }
        public bool BlockedComments { get; }
    }
}