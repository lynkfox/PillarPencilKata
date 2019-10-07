using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilLib;

namespace PencilTests
{
    [TestClass]
    public class PencilTests
    {
        /* Setup Variables */
        public static int length = 500;
        public static int tipDurability = 20;
        public static int eraser = 10;

        public static string testInput = "Test";
        public static string phraseInput = "This is Phrase";
        


        /* Pencil Tip Tests
         */
        [TestMethod]
        public void PencilTipDurabilityIs20()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            int expected = tipDurability;
            int actual = pencil.Tip;
            Assert.AreEqual(expected, actual);
        }

        

        [TestMethod]
        public void DurabilityOfWordTestIs5()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            int expected = 5;
            int actual = pencil.TipDurabilityLoss(testInput);

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


       
        /* Pencil Write Tests */
        [TestMethod]
        public void PencilWriteReturnsString()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            

            Assert.AreEqual("Test", pencil.Write(testInput));

        }

        [TestMethod]
        public void PencilWriteStopsAt0Durabilility()
        {
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            // tip durability is 20. testPhrase costs 14. Run twice
            pencil.Write(phraseInput);  // expected 6 durability left
            pencil.Write(phraseInput); // expected 0 durability, as 6-14 is less than 0

            int expected = 0;
            int actual = pencil.Tip;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PencilWriteReturnsWhiteSpaceifDurabilityRunsOut()
        {
            int lowDurability = 2;
            Pencil pencilLowTip = new Pencil(lowDurability);

            string actual = pencilLowTip.Write(testInput);

            // test input is 5 durability. First letter is a capital, requires 2 durability.
            // expected should be "T   " (3 white spaces)

            string expected = "T   ";
            Assert.AreEqual(expected, actual);
        }


        /* Pencil Sharpen  Tests */
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
            randomPencil.Write(testInput);
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

        
        /* Pencil Eraser Tests*/
        [TestMethod]
        public void DetermineEraserDurabilityLossOfWord()
        {
            
            
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            int expected = 4; // 4 letters 1 point for each letter
            int actual = pencil.EraserDurabilityLoss(testInput);

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

        [TestMethod]
        public void PencilEraseRemovesEraserDurabilityEqualToInputCost()
        {
            
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            int expected = eraser - pencil.EraserDurabilityLoss(testInput); // The Starting Eraser value - the cost of the erase

            pencil.Erase(testInput);

            int actual = pencil.Eraser;

            Assert.AreEqual(expected, actual);

        }
    }
}
