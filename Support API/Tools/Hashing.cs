using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Support_API.Tools
{
    public static class Hashing
    {
        public static String GenerateHash(string password)
        {
            string hash = string.Empty;

            if(password != null)
            {
                using (SHA512 sha512Hash = SHA512.Create())
                {
                    byte[] hashBytes = sha512Hash.ComputeHash(Encoding.ASCII.GetBytes(password));

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        builder.Append(hashBytes[i].ToString("x2"));
                    }
                    hash = builder.ToString();
                }
            }

            if (hash != string.Empty)
                return hash;
            else
                return null;
        }
    }
}
