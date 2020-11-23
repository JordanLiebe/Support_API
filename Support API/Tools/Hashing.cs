using Support_API.Models.Auth;
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
        private static string HashText(string Text)
        {
            string hash = string.Empty;
            if (Text != null)
            {
                using (SHA512 sha512Hash = SHA512.Create())
                {
                    byte[] hashBytes = sha512Hash.ComputeHash(Encoding.ASCII.GetBytes(Text));

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        builder.Append(hashBytes[i].ToString("x2"));
                    }
                    hash = builder.ToString();
                }
            }
            return hash;
        }

        public static String GenerateHash(string Password, int Iterations = 0, string Salt = "")
        {
            if (Iterations == 0)
                Iterations = Generator.RandomNum(1, 1000);

            if (Salt == "")
                Salt = Generator.RandomStr(32);

            Hash hash = new Hash(Iterations, Salt, string.Empty);

            string pass_salt = Password + Salt;
            hash.value = HashText(pass_salt);
            for(int i = 1; i < Iterations; i++)
            {
                hash.value = HashText(hash.value);
            }

            if (hash.value != string.Empty)
                return hash.ToString();
            else
                return null;
        }
    }
}
