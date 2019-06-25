using System;
using System.Text;
using System.Security.Cryptography;

namespace TryFasterClient
{
    class Crypt
    {
        static public string GetHash(string input)//процедура получения хеша
        {
            var sha2 = SHA256.Create();//поздание переменной хеша
            var hash = sha2.ComputeHash(Encoding.UTF8.GetBytes(input));//преобразование хеша
            return Convert.ToBase64String(hash);//вывод хеша
        }
    }
}
