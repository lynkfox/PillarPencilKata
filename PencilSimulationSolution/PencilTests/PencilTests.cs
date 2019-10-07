using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilLib;

namespace PencilTests
{
    [TestClass]
    public class PencilTests
    {
        public static int length = 500;
        public static int tipDurability = 20;
        public static int eraser = 10;

        [TestMethod]
        public void PencilTipDurabilityIs20()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            int expected = tipDurability;
            int actual = pencil.Tip;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PencilWriteReturnsString()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            string input = "Test";
            
            Assert.AreEqual("Test", pencil.Write(input));

        }

        [TestMethod]
        public void DurabilityOfWordTestIs5()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            string input = "Test";
            int expected = 5;
            int actual = pencil.TipDurabilityLoss(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DurabiliytofPhraseIs18()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            string phrase = "She sells sea shells";
            int expected = 18;
            int actual = pencil.TipDurabilityLoss(phrase);

            Assert.AreEqual(expected, actual);
                
        }

        [TestMethod]
        public void WriteWordReducesTipDurability()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            //oops - bad test because of refactoring!!
            string phrase = "Run Run Run";
            int expectedRemainingDurabilityFrom20 = pencil.Tip - pencil.TipDurabilityLoss(phrase);
            pencil.Write(phrase);
            int actual = pencil.Tip;

            Assert.AreEqual(expectedRemainingDurabilityFrom20, actual);
        }

        [TestMethod]
        public void SharpenReturnsToMaxValue()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            int expected = tipDurability; // the starting tipDurability is 20.
            pencil.Sharpen();
            int actual = pencil.Tip;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SharpenReturnsToMaxValueWithRandomDurabilityAfterWrite()
        {

            Random random = new Random();
            int expected = random.Next(0, 100);
            Pencil randomPencil = new Pencil(expected);
            randomPencil.Write("Test");
            randomPencil.Sharpen();
            int actual = randomPencil.Tip;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PencilLengthCanBeReturnedAsInitialized()
        {

            
            Pencil lengthPencil = new Pencil(tipDurability, length);
            int actual = lengthPencil.Length;
            int expected = length;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SharpenReducesPencilLengthBy1()
        {
            
            Pencil lengthPencil = new Pencil(tipDurability, length);
            lengthPencil.Sharpen();
            int actual = lengthPencil.Length;
            int expected = length-1;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PenciEraserReturnsDurability()
        {
            
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            int expected = eraser;
            int actual = pencil.Eraser;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DetermineEraserDurabilityLossOfWord()
        {
            
            string input = "Test";
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            int expected = 4; // 4 letters 1 point for each letter
            int actual = pencil.EraserDurabilityLoss(input);

            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod]
        public void DetermineEraserDurabilityLossOfPhrase()
        {
            string input = "This Test";
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            int expected = 8; // 8 letters, 1 point for each letter, no  cost for white space
            int actual = pencil.EraserDurabilityLoss(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
