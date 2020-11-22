using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Tools
{
    public static class NumberGen
    {
        public static int Random(int min, int max)
        {
            Random rand = new Random();
            int randomNum = rand.Next(min, max);
            return randomNum;
        }
    }
}
