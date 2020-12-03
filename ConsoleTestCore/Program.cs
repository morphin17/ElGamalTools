using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;

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
            byte[] fileBytes = FileToByteArray("./photo_2020-12-02_15-48-28.jpg");
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(fileBytes);
            BigInteger M = new BigInteger(BitConverter.ToUInt64(hash, 0));

            Console.WriteLine(M);
            Console.Read();
            //byte[] M = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            //int[] encodedM = Utils.encode(M, 2);

            //BigInteger p = PrimeNumberGenerator.GeneratePrime(16);
            //Console.WriteLine(p);
            //Console.WriteLine(BitConverter.ToString(p.ToByteArray()));
            //BigInteger g = PrimeNumberGenerator.FindPrimitiveRoot(p);
            //Console.WriteLine(g);
            //Console.WriteLine(BitConverter.ToString(g.ToByteArray()));
            //Console.WriteLine("Good");
            //Console.Read();
        }
    }
}
