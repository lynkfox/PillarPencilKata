using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilLib;

namespace PencilSimulationTests
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

        int expected, actual;


        /* Pencil Tip Tests
         */
        [TestMethod]
        public void PencilTipDurabilityIs20()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            expected = tipDurability;
            actual = pencil.Tip;
            Assert.AreEqual(expected, actual);
        }

        
        /* Depreciated Tests
         * 
         * This method was combined into Write with a refactor. May be pulled out for Clean Code later, but these
         * tests will not be needed for it as it will be a private internal function and these tests will be covered
         * by other Write Tests
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
        */

        [TestMethod]
        public void WriteWordReducesTipDurability()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            //oops - bad test because of refactoring!!
            string phrase = "Run Run Run";
            expected = 8; //12 points for Run Run Run, 20-12 = 8.
            pencil.Write(phrase);
            actual = pencil.Tip;

            Assert.AreEqual(expected, actual);
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

            expected = 0;
            actual = pencil.Tip;
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

        [TestMethod]
        public void PenciWriteAdjustsDurabilityCorrectilyForWhiteSpaceWithMoreLettersThanDurabilityLeft()
        {
            Pencil pencil = new Pencil(tipDurability);
            string longPhrase = "This Phrase Is More Than Twenty Durability"; //43 total durability
            string expectedPhrase = "This Phrase Is More                       "; //20 written durability, 26 white spaces (3 no durability white spaces)
            int expectedTip = 0;

            string actualPhrase = pencil.Write(longPhrase);
            int actualTip = pencil.Tip;

            Assert.AreEqual(expectedPhrase, actualPhrase);
            Assert.AreEqual(expectedTip, actualTip);
        }


        /* Pencil Sharpen  Tests */
        [TestMethod]
        public void SharpenReturnsToMaxValue()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            expected = tipDurability; // the starting tipDurability is 20.
            pencil.Sharpen();
            actual = pencil.Tip;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SharpenReturnsToMaxValueWithRandomDurabilityAfterWrite()
        {

            Random random = new Random();
            expected = random.Next(0, 100);
            Pencil randomPencil = new Pencil(expected);
            randomPencil.Write(testInput);
            randomPencil.Sharpen();
            actual = randomPencil.Tip;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PencilLengthCanBeReturnedAsInitialized()
        {

            
            Pencil lengthPencil = new Pencil(tipDurability, length);
            actual = lengthPencil.Length;
            expected = length;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SharpenReducesPencilLengthBy1()
        {
            
            Pencil lengthPencil = new Pencil(tipDurability, length);
            lengthPencil.Sharpen();
            actual = lengthPencil.Length;
            expected = length-1;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PenciEraserReturnsDurability()
        {
            
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            expected = eraser;
            actual = pencil.Eraser;

            Assert.AreEqual(expected, actual);
        }

        
        /* Pencil Eraser Tests*/
        [TestMethod]
        public void DetermineEraserDurabilityLossOfWord()
        {
            
            
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            expected = 4; // 4 letters 1 point for each letter
            actual = pencil.EraserDurabilityLoss(testInput);

            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod]
        public void DetermineEraserDurabilityLossOfPhrase()
        {
            string input = "This Test";
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            expected = 8; // 8 letters, 1 point for each letter, no  cost for white space
            actual = pencil.EraserDurabilityLoss(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PencilEraseRemovesEraserDurabilityEqualToInputCost()
        {
            
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            expected = eraser - pencil.EraserDurabilityLoss(testInput); // The Starting Eraser value - the cost of the erase

            pencil.Erase(testInput);

            actual = pencil.Eraser;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void PencilEraseReturnsTheWordThatNeedsToBeErased()
        {
            Pencil pencil = new Pencil();
            string expected = testInput;
            string actual = pencil.Erase(testInput);
            Assert.AreEqual(expected, actual);
        }
    }
}
