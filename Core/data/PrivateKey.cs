using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Core.data
{
    [Serializable]
    public class PrivateKey
    {
        private BigInteger _x;

        public PrivateKey(BigInteger x)
        {
            this._x = x;
            
        }

        public BigInteger x
        {
            get
            {
                return _x;
            }
        }

    }
}
