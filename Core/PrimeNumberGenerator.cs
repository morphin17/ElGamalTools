using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Core
{
    public class PrimeNumberGenerator
    {

        public static BigInteger GeneratePrime(int length = 4)
        {
            while (true)
            {
                BigInteger p = new BigInteger(4);
                while (!IsPrime(p, 128))
                {
                    p = GeneratePrimeCandidate(length);
                }

                p = 2 * p + 1;
                if (IsPrime(p, 128))
                {
                    return p;
                }
            }
        }

        public static BigInteger FindPrimitiveRoot(BigInteger p)
        {
            while (true)
            {
                BigInteger alpha = nextRandom(2, p - 1);
                if ((p-1) % alpha != 1)
                {
                    return alpha;
                }
            }
        }

        public static BigInteger GeneratePrimeCandidate(int length)
        {
            var numberBytes = new byte[length];

            Random r = new Random();
            r.NextBytes(numberBytes);
            BigInteger p = new BigInteger(numberBytes);
            byte[] test = p.ToByteArray();
            p = p | ((ulong) (1 << length - 1) | 1);

            return p;
        }

        private static BigInteger nextRandom(BigInteger start, BigInteger end)
        {
            BigInteger r = start;
            Random _r = new Random();
            while (r <= start  || r >= end)
            {
                var numberBytes = new byte[end.ToByteArray().Length];
                _r.NextBytes(numberBytes);
                r = new BigInteger(numberBytes);
                //Console.WriteLine("NextRandomItem: " + r.ToString());
            }

            //Console.WriteLine("NextRandom: " + r.ToString());
            return r;
        }


        public static bool IsPrime(BigInteger n, int k)
        {
            
            if (n == 2  || n == 3)
            {
                return true;
            }

            if (n <= 1 || n % 2 == 0)
            {
                return false;
            }

            BigInteger s = 0;
            BigInteger r = n - 1;
            while ((r & 1) == 0)
            {
                s += 1;
                r /= 2;
            }
                

            for(int i = 0; i < k; i++)
            {
                BigInteger a = nextRandom(2, n - 1);
                BigInteger x = BigInteger.ModPow(a, r, n);

                if (x != 1 && x != n - 1)
                {
                    BigInteger j = 1;
                    while (j < s && x != n - 1)
                    {
                        x = BigInteger.ModPow(a, r, n);
                        if (x == 1){
                            return false;
                        }
                        j += 1;    
                    }

                    if (x != n - 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
