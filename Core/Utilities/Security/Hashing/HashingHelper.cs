using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //Verdiğimiz şifrenin hash'ini oluşturacak metod
        //out ile passwordHash'i ve passwordSalt'ı dışarıya çıkaracağımız yapıyı oluşturuyoruz
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                //key : HMACSHA512 algoritması tarafından oluşturulan benzersiz bir anahtardır. Her kullanıcı için
                //farklı anahtar oluşturulur. Bu anahtara şifreyi çözebilmek için ihtiyacımız var.
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//string'in byte karşılığını alıyoruz
            }
        }

        //Burada kullanıcı sisteme giriş yaptığında, girdiği şifreyi tekrar hash'leyerek oluşturduğumuz passwordHash
        //ile karşılaştırıp doğruluyoruz.
        //Oluşturduğumuz passwordHash'i  doğrulamamız için
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedPasswordHash.Length; i++)
                {
                    if (computedPasswordHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }  
        }
    }
}
