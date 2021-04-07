using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        //Şifreleme olan sistemlerde her şeyi bir byte array tipinde veriyor olmamız gerekiyor.
        //JWT servislerinin anlayabileceği hale getirmemiz gerekiyor.
        //SecurityKey TokenOptions'dan geliyor.
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            //securityKey parametesini kullanarak SymmetricSecurityKey'nin yeni bir örneğini döndürür
        }
    }
}
