using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Core.utils;
using Core.data;
using System.Numerics;

namespace CoreTest
{
    [TestClass]
    public class PairPGTests
    {
        [TestMethod]
        public void CorrectlyLengthGenerated()
        {
            int size = 12;
            PairPG pairPG= new PairPG(size);
            Assert.AreEqual(size, pairPG.prime.ToByteArray().Length);
        }
    }
}
