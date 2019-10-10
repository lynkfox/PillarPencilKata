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
        /* "Test" has a Durability cost of 5 to write, 4 to erase
         */
        public static string phraseInput = "This is Phrase"; 
        /* "This is Phrase" has a Durability cost of 14 to write 12 to erase
         */

        int expected, actual;


        /* Pencil Tip Tests
         */

            /* This test determines that the Pencil Tip is retained
             */
        [TestMethod]
        public void TipDurabilityIs20()
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
         * 
         * 
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

            /* This test determines if Writing a word properly reduces tip Durability based on requirements
             *  2pts per capital
             *  1pt per rest of characters
             *  0pt for whitespace
             */
        [TestMethod]
        public void WriteWordReducesTipDurability()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            string phrase = "Run Run Run";
            expected = 8; //12 points for Run Run Run, 20-12 = 8.
            pencil.Write(phrase);
            actual = pencil.Tip;

            Assert.AreEqual(expected, actual);
        }


       
        /* Pencil Write Tests */

            /*Pencil.Write returns a string because conceptually, a pencil does not store what it is
             * writing, it just writes it down.
             * 
             * This test makes sure Pencil.Write returns what it is writing for the object that will store it
             */
        [TestMethod]
        public void WriteReturnsString()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            
            Assert.AreEqual("Test", pencil.Write(testInput));

        }

        /* This test makes sure that Tip Durability does not go below 0 when it runs out and Write has
         * more characters to write
         */

        [TestMethod]
        public void WriteStopsAt0Durabilility()
        {
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            // tip durability is 20. testPhrase costs 14. Run twice
            pencil.Write(phraseInput);  // expected 6 durability left
            pencil.Write(phraseInput); // expected 0 durability, as 6-14 is less than 0

            expected = 0;
            actual = pencil.Tip;
            Assert.AreEqual(expected, actual);
        }

        /* This test determines that when Writing, if Tip Durability runs out, proper white
         * space is returned for all missing characters that did not have enough durability to write afterward
         * 
         * this test does not test if Capital (2pts) is written with 1pt left
         */
        [TestMethod]
        public void WriteReturnsWhiteSpaceifDurabilityRunsOut()
        {
            int lowDurability = 2;
            Pencil pencilLowTip = new Pencil(lowDurability);

            string actual = pencilLowTip.Write(testInput);

            // test input is 5 durability. First letter is a capital, requires 2 durability.
            // expected should be "T   " (3 white spaces)

            string expected = "T   ";
            Assert.AreEqual(expected, actual);
        }


        /* This test is practically the same as above, but on a larger scale
         */
        [TestMethod]
        public void WriteAdjustsDurabilityCorrectilyForWhiteSpaceWithMoreLettersThanDurabilityLeft()
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

            /*This test determines if Sharpen brings the pencil back to the durability it was first set with
             */
        [TestMethod]
        public void SharpenReturnsToMaxValue()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            expected = tipDurability; // the starting tipDurability is 20.
            pencil.Sharpen();
            actual = pencil.Tip;

            Assert.AreEqual(expected, actual);
        }

        /* This test makes sure it works with any tip durability level
         */
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

        /*This test makes sure that the Pencil Length is stored in the pencil
         */

        [TestMethod]
        public void LengthCanBeReturnedAsInitialized()
        {

            
            Pencil lengthPencil = new Pencil(tipDurability, length);
            actual = lengthPencil.Length;
            expected = length;

            Assert.AreEqual(expected, actual);
        }

        /*This test makes sure that Sharpen properly reduces pencil length
         */
        [TestMethod]
        public void SharpenReducesPencilLengthBy1()
        {
            
            Pencil lengthPencil = new Pencil(tipDurability, length);
            lengthPencil.Sharpen();
            actual = lengthPencil.Length;
            expected = length-1;

            Assert.AreEqual(expected, actual);
        }


        /*This test makes sure that Tip Durability does not change if there is no length left
         */
        [TestMethod]
        public void SharpenPencilWithZeroLengthDoesNotReturnTipToMaximum()
        {
            int zeroPencilLength = 0; 
            /*Objectivly this is a nub that is fully sharpened but has no length left so is OK
             * in terms of Pencil
             */
            Pencil pencil = new Pencil(tipDurability, zeroPencilLength, eraser);
            pencil.Write(testInput); 
            expected = 15;

            pencil.Sharpen();

            actual = pencil.Tip;

            Assert.AreEqual(expected, actual);
        }




        /* Pencil Eraser Tests*/

            /*This determines if eraser durability is saved
             */
        [TestMethod]
        public void EraserReturnsDurability()
        {
            
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            expected = eraser;
            actual = pencil.Eraser;

            Assert.AreEqual(expected, actual);
        }

        /* These two tests are depreciated because this function is now private. Other tests should 
         * properly cover it
        
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
        */


        /* This test is similar to the pencil.write test that returns the word passed to it, because again
         * Pencils do not store what they write, they only write it or erase it. Storing it is for another object
         * 
         */
        [TestMethod]
        public void EraseReturnsTheWordThatNeedsToBeErased()
        {
            Pencil pencil = new Pencil();
            string expected = testInput;
            string actual = pencil.Erase(testInput);
            Assert.AreEqual(expected, actual);
        }


        /*If there is no Eraser, Don't erase anything!
         */
        [TestMethod]
        public void EraserIsZeroReturnNothingToBeErased()
        {
            int zeroEraserDurability = 0;
            Pencil pencil = new Pencil(tipDurability, length, zeroEraserDurability);
            string expected = "";
            string actual = pencil.Erase(testInput);
            Assert.AreEqual(expected, actual);

        }

        /*This test determines if Pencil.Erase proprely removes durability 
         * 
         * (Replacing durability tests above)
         */

        [TestMethod]
        public void EraseProperlyRemovesDurabilityOfErasedItem()
        {
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            
            expected = 6; //10 eraser, Test is 4 cost, 6 remainder
            pencil.Erase(testInput);
            actual = pencil.Eraser;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EraseErasesBackwardFromEndOfInputAndLeavesWhiteSpaceOnWhat()
        {
            int eraserWithTwoDurability = 2;
            Pencil pencil = new Pencil(tipDurability, length, eraserWithTwoDurability);
            string expected = "st"; 
            // Test has 4 durability, 2 durability eraser will erase s and t from the end

            string actual = pencil.Erase(testInput);

            Assert.AreEqual(expected, actual);
        }
    }
}
