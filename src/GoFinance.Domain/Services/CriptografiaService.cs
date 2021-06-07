using GoFinance.Domain.Interfaces.Services;
using System;
using System.Security.Cryptography;
using System.Text;

namespace GoFinance.Domain.Services
{
    public class CriptografiaService : ICriptografiaService
    {
        private string SALT = "76C1A59D3B3748BB913CC28AE556D0E9";

        public byte[] Encrypt(string text)
        {
            while (SALT.Length < 6)
            {
                SALT += SALT + "Z";
            }

            using var sha = SHA512.Create();
            var SALT2 = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(SALT[^4..])));

            return sha.ComputeHash(Encoding.UTF8.GetBytes(text + SALT2));
        }
    }
}
