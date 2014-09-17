using System;
using System.Collections.Generic;
using System.Text;

namespace TempApps
{
    class Program
    {
        public static long getDecimalNum(string numStr)
        {
            if (numStr == null || numStr.Length == 0)
                return 0;
            if (numStr.IndexOf('#') == 0)
                numStr = numStr.Substring(1);
            long l = 0;
            char[] chs = numStr.ToCharArray();
            for (int i = numStr.Length - 1; i >= 0; i--)
            {
                l = l + transferSingleFrom16(numStr.Substring(i,1))* (getExp(16, numStr.Length - i -1));
                //l = l + chs[i];
            }
            return l;
        }
        public static long transferSingleFrom16(string numStr)
        {
            if ("0123456789".IndexOf(numStr) >= 0)
                return long.Parse(numStr);
            if ("a" == numStr.ToLower())
                return 10;
            if ("b" == numStr.ToLower())
                return 11;
            if ("c" == numStr.ToLower())
                return 12;
            if ("d" == numStr.ToLower())
                return 13;
            if ("e" == numStr.ToLower())
                return 14;
            if ("f" == numStr.ToLower())
                return 15;
            return -1;
        }
        public static long getExp(long x, long y)
        {
            if (y < 0)
                return 0;
            if (y == 0)
                return 1;
            for (int i = 0; i < y -1; i++)
            {
                x = x * x;
            }
            return x;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("The decimal is : " + getDecimalNum("66a"));
            Console.ReadLine();
        }
    }
}
