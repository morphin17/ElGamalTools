using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography; 

namespace Core.utils
{
    public class Encoder
    {
        static public BigInteger EncodeByteArray(byte[] fileBytes)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(fileBytes);
            return new BigInteger(BitConverter.ToUInt64(hash, 0));
        }
    }
}
