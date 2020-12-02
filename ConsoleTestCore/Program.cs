using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using System.IO;
using System.Numerics;

namespace ConsoleTestCore
{
    class Program
    {
        public static  byte[] FileToByteArray(string fileName)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(fileName,
                                           FileMode.Open,
                                           FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(fileName).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }

        static void Main(string[] args)
        {
            //byte[] M = FileToByteArray("./photo_2020-12-02_15-48-28.jpg");
            //byte[] M = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            //int[] encodedM = Utils.encode(M, 2);
            BigInteger p = PrimeNumberGenerator.GeneratePrime(2);
            BigInteger g = PrimeNumberGenerator.FindPrimitiveRoot(p);
            Console.WriteLine(p);
            Console.WriteLine(BitConverter.ToString(p.ToByteArray()));
            Console.WriteLine(g);
            Console.WriteLine(BitConverter.ToString(g.ToByteArray()));
            Console.WriteLine("Good");
            Console.Read();
        }
    }
}
