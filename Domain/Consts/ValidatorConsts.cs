using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consts
{
    public static class ValidatorConsts
    {
        public const string NotEmpty = "You must enter {0}.";
        public const string MinimumLength = "You must enter at least {0} characters.";
        public const string MaximumLength = "Maximum length is {0} characters.";
        public const string Equal = "You must enter the same as in the {0}.";
        public const string EmailAddress = "Incorrect email address.";
    }
}
