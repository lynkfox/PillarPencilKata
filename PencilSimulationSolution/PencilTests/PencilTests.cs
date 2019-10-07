using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilLib;

namespace PencilTests
{
    [TestClass]
    public class PencilTests
    {

        public static int tipDurability = 20;
        public static Pencil pencil = new Pencil(tipDurability);

        [TestMethod]
        public void PencilTipDurabilityIs20()
        {
            Assert.AreEqual(20, pencil.tip);
        }

        [TestMethod]
        public void PencilWriteReturnsString()
        {
            string input = "Test";
            
            Assert.AreEqual("Test", pencil.write(input));

        }

        [TestMethod]
        public void DurabilityOfWordTestIs5()
        {
            string input = "Test";
            int expected = 5;
            int actual = pencil.tipDruabilityLoss(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DurabiliytofPhraseIs18()
        {
            string phrase = "She sells sea shells";
            int expected = 18;
            int actual = pencil.tipDruabilityLoss(phrase);

            Assert.AreEqual(expected, actual);
                
        }

        [TestMethod]
        public void WriteWordReducesTipDurability()
        {
            string phrase = "Run Run Run";
            int expectedRemainingDurabilityFrom20 = 8;
            pencil.write(phrase);
            int actual = pencil.tip;

            Assert.AreEqual(expectedRemainingDurabilityFrom20, actual);
        }

        [TestMethod]
        public void SharpenReturnsToMaxValue()
        {
            int expected = tipDurability; // the starting tipDurability is 20.
            pencil.Sharpen();
            int actual = pencil.tip;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SharpenReturnsToMaxValueWithRandomDurabilityAfterWrite()
        {
            Random random = new Random();
            int expected = random.Next(0, 100);
            Pencil randomPencil = new Pencil(expected);
            pencil.write("Test");
            pencil.Sharpen();
            int actual = pencil.tip;

            Assert.AreEqual(expected, actual);
        }
    }
}
