using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Core.data
{
    [Serializable]
    public class Signature
    {
        private BigInteger _r;
        private BigInteger _s;

        public Signature(BigInteger r, BigInteger s)
        {
            _r = r;
            _s = s;
        }

        public BigInteger r
        {
            get
            {
                return _r;
            }
        }

        public BigInteger s
        {
            get
            {
                return _s;
            }
        }
    }
}
