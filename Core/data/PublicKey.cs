using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace Core.data 
{
    [Serializable]
    public class PublicKey
    {
        private PairPG _pairPG;
        private BigInteger _h;

        public PublicKey(PairPG pairPG, BigInteger h)
        {
            this._pairPG = pairPG;
            this._h = h;
        }

        public PairPG pairPG
        {
            get
            {
                return _pairPG;
            }
        }

        public BigInteger h
        {
            get
            {
                return _h;
            }
        }

    }
}
