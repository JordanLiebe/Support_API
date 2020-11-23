using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Models.Auth
{
    public class Hash
    {
        public Hash(string HashValue)
        {
            string[] HashBreak = HashValue.Split("~");
            iterations = Int32.Parse(HashBreak[0]);
            salt = HashBreak[1];
            value = HashBreak[2];
        }
        public Hash(int Iterations, string Salt, string Value)
        {
            iterations = Iterations;
            salt = Salt;
            value = Value;
        }
        public override string ToString()
        {
            return $"{iterations}~{salt}~{value}";
        }
        public int iterations { get; set; }
        public string salt { get; set; }
        public string value { get; set; }
    }
}
