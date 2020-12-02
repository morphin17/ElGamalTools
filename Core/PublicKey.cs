using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class PublicKey
    {
        public ulong p;
        public ulong g;
        public ulong h;
        public ulong iNumBits;

        public PublicKey(ulong p, ulong g, ulong h, ulong iNumBits = 0)
        {
            this.p = p;
            this.g = g;
            this.h = h;
            this.iNumBits = iNumBits;
        }
    }
}
