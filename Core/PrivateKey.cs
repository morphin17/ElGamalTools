using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class PrivateKey
    {
        public ulong p;
        public ulong g;
        public ulong x;
        public ulong iNumBits;

        public PrivateKey(ulong p, ulong g, ulong x, ulong iNumBits = 0)
        {
            this.p = p;
            this.g = g;
            this.x = x;
            this.iNumBits = iNumBits;
        }
    }
}
