using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Core.data;
using Core.utils;
using System.Numerics;
using System.Text;

namespace CoreTest
{
    [TestClass]
    public class ElGamalTests
    {

        [TestMethod]
        public void CorrectlyCheckSign()
        {
            PairPG pairPG = new PairPG();
            ElGamal elGamal = new ElGamal(pairPG);

            string data = "Test message";
            byte[] dataBytes = Encoding.ASCII.GetBytes(data);

            Signature sign = elGamal.Generate(dataBytes);

            Assert.IsTrue(elGamal.Verify(dataBytes, sign));
        }

        [TestMethod]
        public void CorrectlyCheckSignWithotPrivateKey()
        {
            PairPG pairPG = new PairPG();
            ElGamal elGamal = new ElGamal(pairPG);

            string data = "Test message";
            byte[] dataBytes = Encoding.ASCII.GetBytes(data);

            Signature sign = elGamal.Generate(dataBytes);
            ElGamal elGamal2 = new ElGamal(publicKey: elGamal.publicK);
            Assert.IsTrue(elGamal2.Verify(dataBytes, sign));
        }

        [TestMethod]
        public void NotCorrectlyCheckSignDifferentPairPG()
        {
            PairPG pairPG = new PairPG();
            ElGamal elGamal = new ElGamal(pairPG);

            string data = "Test message";
            byte[] dataBytes = Encoding.ASCII.GetBytes(data);

            Signature sign = elGamal.Generate(dataBytes);
            PairPG pairPG2 = new PairPG();
            ElGamal elGamal2 = new ElGamal(pairPG2);

            Assert.IsFalse(elGamal2.Verify(dataBytes, sign));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GenerateSignWithoutPrivateKey()
        {
            PairPG pairPG = new PairPG();
            ElGamal elGamal = new ElGamal(pairPG);

            
            PairPG pairPG2 = new PairPG();
            ElGamal elGamal2 = new ElGamal(publicKey: elGamal.publicK);

            string data = "Test message";
            byte[] dataBytes = Encoding.ASCII.GetBytes(data);

            Signature sign = elGamal2.Generate(dataBytes);
        }


    }
}
