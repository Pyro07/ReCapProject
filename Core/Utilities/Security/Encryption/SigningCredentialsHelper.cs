using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        //WebApi'nin kullanabileceği JWT tokenlerının doğrulayabilmesi için 
        //SigningCredentials : Dijital imza oluşturmak için kullanılan şifreleme anahtarını ve güvenlik
        //algoritmalarını temsil eder.
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
