using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Cryptography;
using System.Text;

namespace WorkFlowX.Services
{
    public static class UtilityService
    {
        public static string ConvertToSHA256(string texto)
        {
            string hash = string.Empty;

            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] hasValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));

                foreach(byte b in hasValue)
                {
                    hash += $"{b:X2}";
                }
                return hash;
            }
        }

        public static string GenerateToken()
        {
            string token = Guid.NewGuid().ToString("N");
            return token;
        }
    }
}