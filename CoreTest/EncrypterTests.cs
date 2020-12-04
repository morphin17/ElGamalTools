using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Core.data;
using Core.utils;
using System.Linq;
using System.Security.Cryptography;

namespace CoreTest
{
    [TestClass]
    public class EncrypterTests
    {
        [TestMethod]
        public void CorrectlyDecrypt()
        {
            PairPG pairPG = new PairPG();
            ElGamal elGamal = new ElGamal(pairPG);

            byte[] encrypted = Encrypter.EncryptBytes(Ser.GetBytes(elGamal.privateK), Core.utils.Encoder.Encode("password"), Core.utils.Encoder.Encode("elgamal-tools"));
            byte[] decrypted = Encrypter.DecryptBytes(encrypted, Core.utils.Encoder.Encode("password"), Core.utils.Encoder.Encode("elgamal-tools"));

            Assert.IsTrue(decrypted.SequenceEqual(Ser.GetBytes(elGamal.privateK)));
        }

        [TestMethod]
        public void NotCorrectlyDecryptBadIV()
        {
            PairPG pairPG = new PairPG();
            ElGamal elGamal = new ElGamal(pairPG);

            byte[] encrypted = Encrypter.EncryptBytes(Ser.GetBytes(elGamal.privateK), Core.utils.Encoder.Encode("password"), Core.utils.Encoder.Encode("elgamal-tools"));
            byte[] decrypted = Encrypter.DecryptBytes(encrypted, Core.utils.Encoder.Encode("password"), Core.utils.Encoder.Encode("elgamal-tools2"));

            Assert.IsFalse(decrypted.SequenceEqual(Ser.GetBytes(elGamal.privateK)));
        }

        [TestMethod]
        [ExpectedException(typeof(CryptographicException))]
        public void NotCorrectlyDecryptBadKey()
        {
            PairPG pairPG = new PairPG();
            ElGamal elGamal = new ElGamal(pairPG);

            byte[] encrypted = Encrypter.EncryptBytes(Ser.GetBytes(elGamal.privateK), Core.utils.Encoder.Encode("password"), Core.utils.Encoder.Encode("elgamal-tools"));
            byte[] decrypted = Encrypter.DecryptBytes(encrypted, Core.utils.Encoder.Encode("password2"), Core.utils.Encoder.Encode("elgamal-tools"));

        }
    }
}
