using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Tools
{
    public static class Generator
    {
        public static int RandomNum(int min, int max)
        {
            Random rand = new Random();
            int randomNum = rand.Next(min, max);
            return randomNum;
        }

        public static string RandomStr(int length)
        {
            string RandStr = string.Empty;

            for(int i = 0; i < length; i++)
            {
                char character = (char)'!';
                int offset = RandomNum(0, 92);
                character = (char)(character + offset);
                RandStr += character;
            }

            return RandStr;
        }
    }
}
