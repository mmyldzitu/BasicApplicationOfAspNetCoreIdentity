using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.CustomDescriber
{
    public class CustomErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code= "PasswordTooShort",
            Description=$" Parola oluşturmak için en az {length} karakter kullanılmalıdır"};
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new() { Code = "PasswordRequiresNonAlphanumeric", Description = "Parola en az bir adet alfanumerik karakter içermelidir (~!-vs.)" };
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new() { Code= "DuplicateUserName", Description=$"{userName} kullanıcı adı kullanımda"};
        }
    }
}
