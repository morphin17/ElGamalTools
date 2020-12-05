using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Core.data
{


    [Serializable]
    public class PairPG 
    {
        
        private BigInteger p;
        private BigInteger g;

        
        //private Func<BigInteger, int> onNewCandidate;
        
        //private Func<BigInteger, int> onNewPrime;
        [field: NonSerialized]
        private System.ComponentModel.BackgroundWorker worker;
        public PairPG(
            int length = 8,
            System.ComponentModel.BackgroundWorker worker = null
            ) : base()
        {
            this.worker = worker;

            GeneratePrime(length);
            FindPrimitiveRoot();
        }

        public BigInteger prime
        {
            get
            {
                return p;
            }
        }

        public BigInteger generator
        {
            get
            {
                return g;
            }
        }


        private void GeneratePrime(int length)
        {
            while (true)
            {
                BigInteger p = new BigInteger(4);
                while (!IsPrime(p, 128))
                {
                    p = GeneratePrimeCandidate(length);
                    if (worker != null)
                    {
                        if (worker.CancellationPending)
                        {
                            return;
                        }
                    }
                }


                if (worker != null)
                {
                    worker.ReportProgress(0, p);
                }

                if (IsPrime((p - 1) / 2, 128))
                {
                    this.p = p;
                    return;
                }
            }
        }

        private void FindPrimitiveRoot()
        {
            BigInteger p1 = 2;
            BigInteger p2 = (p - 1) / p1;

            while (true)
            {
                if (worker != null)
                {
                    if (worker.CancellationPending)
                    {
                        return;
                    }
                }

                BigInteger g = utils.BigIntegerRandom.nextRandom(2, p - 1);

                if (worker != null)
                {
                    worker.ReportProgress(1, g);
                }

                if ((BigInteger.ModPow(g, (p - 1) / p1, p) != 1) &&
                   (BigInteger.ModPow(g, (p - 1) / p2, p) != 1))
                {
                    this.g = g;
                    return;
                }
            }
        }

        private static BigInteger GeneratePrimeCandidate(int length)
        {
            BigInteger p = utils.BigIntegerRandom.nextRandom(length);
            p = p | ((ulong)(1 << length - 1) | 1);
            while (p < 0)
            {
                p = utils.BigIntegerRandom.nextRandom(length);
                p = p | ((ulong)(1 << length - 1) | 1);
            }

            return p;
        }

        public static bool IsPrime(BigInteger n, int k)
        {

            if (n == 2 || n == 3)
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


            for (int i = 0; i < k; i++)
            {
                BigInteger a = utils.BigIntegerRandom.nextRandom(2, n - 1);
                BigInteger x = BigInteger.ModPow(a, r, n);

                if (x != 1 && x != n - 1)
                {
                    BigInteger j = 1;
                    while (j < s && x != n - 1)
                    {
                        x = BigInteger.ModPow(a, r, n);
                        if (x == 1) {
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
