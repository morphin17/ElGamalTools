using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.utils;
using Core.data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Numerics;

namespace ElGamalTools
{
    public class Utils
    {
        public static List<PairPG> GetPairPGs()
        {
            FileStream fs = new FileStream("pgList.dat", FileMode.Open);
            BinaryFormatter binForm = new BinaryFormatter();
            List<PairPG> list = (List<PairPG>)binForm.Deserialize(fs);
            return list;
        }

        public static string BigIntegerToHexString(BigInteger p)
        {
            return BitConverter.ToString(p.ToByteArray()).Replace("-", "");
        }
    }
}
