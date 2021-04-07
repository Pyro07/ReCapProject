using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        //İlgili kullanıcımız için kullanıcının claim'lerini içerecek token'ı oluşturacak metod
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims); 
    }
}
