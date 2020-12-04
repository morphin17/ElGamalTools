using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Core.utils
{
    class BigIntegerUtils
    {
        public static BigInteger gcd(BigInteger a, BigInteger b)
        {
            if (a < b)
            {
                return gcd(b, a);
            }

            BigInteger c;
            while (b != 0)
            {
                c = a % b;
                a = b;
                b = c;
            }

            return a;
        }

        public static BigInteger modInverse(BigInteger a, BigInteger m) 
        {
            BigInteger x = 0;
            BigInteger y = 0;
            BigInteger g = egcd(a, m, ref x, ref y);

            if (g != 1)
            {
                return -1;
            }
            else
            {
                return (x % m + m) % m;
            }
        }

        public static BigInteger egcd(BigInteger a, BigInteger b, ref BigInteger x, ref BigInteger y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            BigInteger x1 = 0, y1 = 0;
            BigInteger gcd = egcd(b % a, a, ref x1, ref y1);

            x = y1 - (b / a) * x1;
            y = x1;

            return gcd;
        }

    }
}
