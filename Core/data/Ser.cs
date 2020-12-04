using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Core.data
{
    public class Ser
    {
        static public byte[] GetBytes(PairPG pairPG)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, pairPG);
            return ms.ToArray();
        }

        static public byte[] GetBytes(PrivateKey privateKey)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, privateKey);
            return ms.ToArray();
        }

        static public byte[] GetBytes(PublicKey publicKey)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, publicKey);
            return ms.ToArray();
        }

        static public byte[] GetBytes(Signature publicKey)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, publicKey);
            return ms.ToArray();
        }
    }
}
