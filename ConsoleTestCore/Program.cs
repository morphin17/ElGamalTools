using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.utils;
using Core.data;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleTestCore
{
    class Program
    {

        public static byte[] FileToByteArray(string fileName)
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


        static int candidateTest(BigInteger p)
        {
            Console.WriteLine("Candidate: " + p);
            return 1;
        }

        static int primeTest(BigInteger p)
        {
            Console.WriteLine("Prime: " + p);
            return 1;
        }


        static void generatePairsPG()
        {
            List<PairPG> data = new List<PairPG>();

            for (int i = 8; i <= 32; i++)
            {
                Console.WriteLine(i);
                PairPG pairPG = new PairPG(i);
                data.Add(pairPG);
                Console.WriteLine(data.Last().prime);
                Console.WriteLine(data.Last().generator);
                Console.WriteLine("==================================");
            }

            FileStream fs = new FileStream("pgList.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, data);
            fs.Close();
        }

        static PairPG getPairPG(int length)
        {
            FileStream fs = new FileStream("pgList.dat", FileMode.Open);
            BinaryFormatter binForm = new BinaryFormatter();
            List<PairPG> list = (List<PairPG>)binForm.Deserialize(fs);
            return list[length / 8 - 8];
        }

        static void Main(string[] args)
        {
            Console.WriteLine(getPairPG(256).prime);

            //byte[] fileBytes = FileToByteArray("./photo_2020-12-02_15-48-28.jpg");
            //var md5 = MD5.Create();
            //var hash = md5.ComputeHash(fileBytes);
            //BigInteger M = new BigInteger(BitConverter.ToUInt64(hash, 0));

            //Console.WriteLine(M);
            //Console.Read();
            //byte[] M = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            //int[] encodedM = Utils.encode(M, 2);

            //PairPG pairPG = new PairPG();
            //ElGamal elGamal = new ElGamal(pairPG);

            //string data = "Сообщение, что же я тут написал ну хз хз?";
            //byte[] dataBytes = Encoding.ASCII.GetBytes(data);

            //Signature sign = elGamal.Generate(dataBytes);

            //byte[] encrypted = Encrypter.EncryptBytes(Ser.GetBytes(elGamal.privateK), Core.utils.Encoder.Encode("password"), Core.utils.Encoder.Encode("elgamal-tools"));
            //byte[] decrypted = Encrypter.DecryptBytes(encrypted, Core.utils.Encoder.Encode("password"), Core.utils.Encoder.Encode("elgtool"));


            //PrivateKey privateKey = Des.toPrivateKey(decrypted);

            //Console.WriteLine( decrypted.SequenceEqual(Ser.GetBytes(elGamal.privateK)));

            //PairPG pairPG2 = new PairPG();
            //ElGamal elGamal2 = new ElGamal(pairPG2);
            //string data2 = "Сообщение, что же я тут написал ну хз хз?";
            //byte[] dataBytes2 = Encoding.ASCII.GetBytes(data2);

            //Signature sign2 = elGamal2.Generate(dataBytes2);

            //ElGamal elGamal3 = new ElGamal(publicKey: elGamal.publicK);
            //Signature sign3 = elGamal3.Generate(dataBytes2);

            //if (elGamal3.Verify(dataBytes, sign))
            //{
            //    Console.WriteLine("Подпись действительна");
            //}
            //else
            //{
            //    Console.WriteLine("Подпись не действительна");
            //}



            //PrivateKey privateKey = new PrivateKey(1);
            //byte[] r = Ser.GetBytes(privateKey);
            //PrivateKey privateKeyClone = Des.toPrivateKey(r);

            //Console.WriteLine(p);
            //Console.WriteLine(BitConverter.ToString(p.ToByteArray()));
            //Console.WriteLine(g);
            //Console.WriteLine(BitConverter.ToString(g.ToByteArray()));
            Console.Read();
        }
    }
}
