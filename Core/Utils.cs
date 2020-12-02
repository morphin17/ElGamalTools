using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Utils
    {
        /// <summary>
        /// Вычисляет НОД
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ulong gcd(ulong a, ulong b)
        {   
            if (a < b)
            {
                return gcd(b, a);
            }

            ulong c;
            while (b != 0)
            {
                c = a % b;
                a = b;
                b = c;
            }

            return a;
        }

        public static int[] encode(byte[] byte_array, int iNumBits)
        {
            List<int> z = new List<int>();

            int k = iNumBits / 8;
            int j = -1 * k;

            for(int i = 0; i < byte_array.Length; i++)
            {
                if (i % k == 0)
                {
                    j += k;
                    z.Add(0);
                }

                z[j / k] += byte_array[i] * (int) Math.Pow(2, 8 * (i%k));

            }

            return z.ToArray();

        }
    }
}
