using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.data;
using System.Numerics;

namespace Core.utils
{
    public class ElGamal
    {
        private PrivateKey privateKey;
        private PublicKey publicKey;

        public ElGamal(PairPG pairPG)
        {
            privateKey = new PrivateKey(utils.BigIntegerRandom.nextRandom(1, pairPG.prime - 2));
            publicKey = new PublicKey(pairPG, BigInteger.ModPow(pairPG.generator, privateKey.x, pairPG.prime));

        }

        public ElGamal(PrivateKey privateKey = null, PublicKey publicKey = null)
        {
            this.privateKey = privateKey;
            this.publicKey = publicKey;
        }

        public PrivateKey privateK
        {
            get
            {
                return privateKey;
            }
        }

        public PublicKey publicK
        {
            get
            {
                return publicKey;
            }
        }

        public Signature Generate(byte[] fileBytes)
        {
            if (privateKey == null)
            {
                throw new Exception();
            }

            BigInteger M = Encoder.EncodeByteArray(fileBytes);
            BigInteger k;

            while (true)
            {
                k = utils.BigIntegerRandom.nextRandom(1, publicKey.pairPG.prime - 2);
                if (utils.BigIntegerUtils.gcd(k, publicKey.pairPG.prime - 1) == 1)
                {
                    break;
                }
            }

            BigInteger r = BigInteger.ModPow(publicKey.pairPG.generator, k, publicKey.pairPG.prime);

            BigInteger l = utils.BigIntegerUtils.modInverse(k, publicKey.pairPG.prime - 1);

            if (l < 0)
            {
                throw new Exception();
            }

            BigInteger s = l * (M - privateKey.x * r) % (publicKey.pairPG.prime - 1);
            
            if (s < 0)
            {
                s += publicKey.pairPG.prime - 1;
            }

            return new Signature(r, s);
        }

        public bool Verify(byte[] fileBytes, Signature sign)
        {
            if (sign.r < 1 || sign.r > publicKey.pairPG.prime -1)
            {
                return false;
            }

            BigInteger M = Encoder.EncodeByteArray(fileBytes);
            BigInteger v1 = BigInteger.ModPow(publicKey.h, sign.r, publicKey.pairPG.prime) * BigInteger.ModPow(sign.r, sign.s, publicKey.pairPG.prime);
            v1 = v1 % publicKey.pairPG.prime;
            BigInteger v2 = BigInteger.ModPow(publicKey.pairPG.generator, M, publicKey.pairPG.prime);

            return v1 == v2;
        }

    }
}
