using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Core.utils
{
    public class BigIntegerRandom
    {
        public static Random _r = new Random();

        public static BigInteger nextRandom(BigInteger start, BigInteger end)
        {
            BigInteger r = start;
            while (r <= start || r >= end)
            {
                var numberBytes = new byte[end.ToByteArray().Length];
                _r.NextBytes(numberBytes);
                r = new BigInteger(numberBytes);
            }

            return r;
        }

        public static BigInteger nextRandom(int length)
        {
            var numberBytes = new byte[length];
            _r.NextBytes(numberBytes);
            BigInteger r = new BigInteger(numberBytes);
            
            return r;
        }

    }
}
