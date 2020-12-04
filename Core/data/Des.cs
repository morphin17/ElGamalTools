using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Core.data
{
    public class Des
    {

        static public PairPG toPairPG(byte [] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            PairPG obj = (PairPG)binForm.Deserialize(memStream);
            return obj;
        }

        static public PrivateKey toPrivateKey(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            PrivateKey obj = (PrivateKey)binForm.Deserialize(memStream);
            return obj;
        }

        static public PublicKey toPublicKey(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            PublicKey obj = (PublicKey)binForm.Deserialize(memStream);
            return obj;
        }

        static public Signature toSignature(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Signature obj = (Signature)binForm.Deserialize(memStream);
            return obj;
        }
    }
}
