using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilLib;
using System;

namespace PencilSimulationTests
{
    [TestClass]
    public class PencilTests
    {
        public int length = 500;
        public int tipDurability = 20;
        public int eraser = 10;

        public string singleWord5toWrite4toErase = "Test";
        public int tipDurabilityAfterSingle = 15;
        public int eraserDurabilityAfterSingle = 6;

        public string phrase14toWrite12toErasewithWhitespace = "This is Phrase";
        public int tipDurabilityAfterPhrase = 6;
        public int eraserDurabilityAfterPhrase = 0;

        int expectedInt, actualInt;
        string expectedString, actualString;


        /* Pencil Tip Tests
         */

        [TestMethod]
        public void TipDurabilityIs20()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            expectedInt = tipDurability;

            actualInt = pencil.Tip;

            Assert.AreEqual(expectedInt, actualInt);
        }

        
        [TestMethod]
        public void WriteWordReducesTipDurability()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            expectedInt = tipDurabilityAfterPhrase;
            pencil.Write(phrase14toWrite12toErasewithWhitespace);
            actualInt = pencil.Tip;

            Assert.AreEqual(expectedInt, actualInt);
        }


       
        /* Pencil Write Tests */

   
        [TestMethod]
        public void WriteReturnsString()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            expectedString = singleWord5toWrite4toErase;

            actualString = pencil.Write(singleWord5toWrite4toErase);
            
            Assert.AreEqual(expectedString, actualString);

        }


        [TestMethod]
        public void WriteStopsAt0Durabilility()
        {
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            expectedInt = 0;

            // tip durability is 20. testPhrase costs 14. Run twice
            pencil.Write(phrase14toWrite12toErasewithWhitespace);  // expected 6 durability left
            pencil.Write(phrase14toWrite12toErasewithWhitespace); // expected 0 durability, as 6-14 is less than 0
            actualInt = pencil.Tip;

            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void WriteReturnsWhiteSpaceifDurabilityRunsOut()
        {
            int lowDurability = 2;
            Pencil pencilLowTip = new Pencil(lowDurability);
            expectedString = "T   ";

            actualString = pencilLowTip.Write(singleWord5toWrite4toErase);

            // test input is 5 durability. First letter is a capital, requires 2 durability.
            // expected should be "T   " (3 white spaces)

            
            Assert.AreEqual(expectedString, actualString);
        }


        /* This test is practically the same as above, but on a larger scale
         */
        [TestMethod]
        public void WriteAdjustsDurabilityCorrectilyForWhiteSpaceWithMoreLettersThanDurabilityLeft()
        {
            Pencil pencil = new Pencil(tipDurability);
            string longPhrase = "This Phrase Is More Than Twenty Durability"; //43 total durability
            expectedString = "This Phrase Is More                       "; //20 written durability, 26 white spaces (3 no durability white spaces)
            int expectedTip = 0;

            actualString = pencil.Write(longPhrase);
            int actualTip = pencil.Tip;

            Assert.AreEqual(expectedString, actualString);
            Assert.AreEqual(expectedTip, actualTip);
        }

        [TestMethod]
        public void WriteWithZeroDurabilityReturnsProperNumberOfWhiteSpace()
        {
            Pencil pencil = new Pencil();
            pencil.Tip = 0;
            string expected = "    "; //T E S T - 4 letters, 4 white space

            string actual = pencil.Write(singleWord5toWrite4toErase);

            Assert.AreEqual(expected, actual);



        }

        [TestMethod]
        public void WriteCapitalLetterWithOnePointDurabilityReturnsCapitalLetterAnd0Durability()
        {
            Pencil pencil = new Pencil(1);
            expectedString = "T";
            expectedInt = 0;

            

            actualString = pencil.Write("T");
            actualInt = pencil.Tip;

            Assert.AreEqual(expectedInt, actualInt);
            Assert.AreEqual(expectedString, actualString);



        }


        /* Pencil Sharpen  Tests */

        [TestMethod]
        public void SharpenReturnsToMaxValue()
        {
            Pencil pencil = new Pencil(tipDurability, length);
            expectedInt = tipDurability; // the starting tipDurability is 20.

            pencil.Sharpen();
            actualInt = pencil.Tip;

            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void SharpenReturnsToMaxValueWithRandomDurabilityAfterWrite()
        {

            Random random = new Random();
            expectedInt = random.Next(0, 100);
            Pencil randomPencil = new Pencil(expectedInt);

            randomPencil.Write(singleWord5toWrite4toErase);
            randomPencil.Sharpen();
            actualInt = randomPencil.Tip;

            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void LengthCanBeReturnedAsInitialized()
        {
            Pencil lengthPencil = new Pencil(tipDurability, length);
            expectedInt = length;

            actualInt = lengthPencil.Length;

            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void SharpenReducesPencilLengthBy1()
        {
            
            Pencil lengthPencil = new Pencil(tipDurability, length);
            expectedInt = length - 1;

            lengthPencil.Sharpen();
            actualInt = lengthPencil.Length;
            

            Assert.AreEqual(expectedInt, actualInt);
        }

        /*Objectivly this is a nub that is fully sharpened but has no length left so is OK
             * in terms of Pencil
             */
        [TestMethod]
        public void SharpenPencilWithZeroLengthDoesNotReturnTipToMaximum()
        {
            int zeroPencilLength = 0; 
            
            Pencil pencil = new Pencil(tipDurability, zeroPencilLength, eraser);
            pencil.Write(singleWord5toWrite4toErase); //Starting durability is 20, testInput costs 5 durability
            expectedInt = 15;

            pencil.Sharpen();

            actualInt = pencil.Tip;

            Assert.AreEqual(expectedInt, actualInt);
        }




        /* Pencil Eraser Tests*/

  
        [TestMethod]
        public void EraserReturnsDurability()
        {
            
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            expectedInt = eraser;

            actualInt = pencil.Eraser;

            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void EraseReturnsTheWordThatNeedsToBeErased()
        {
            Pencil pencil = new Pencil();
            expectedString = singleWord5toWrite4toErase;

            actualString = pencil.Erase(singleWord5toWrite4toErase);

            Assert.AreEqual(expectedString, actualString);
        }


        [TestMethod]
        public void EraserIsZeroReturnNothingToBeErased()
        {
            int zeroEraserDurability = 0;
            Pencil pencil = new Pencil(tipDurability, length, zeroEraserDurability);
            string expected = "";

            string actual = pencil.Erase(singleWord5toWrite4toErase);

            Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        public void EraseProperlyRemovesDurabilityOfErasedItem()
        {
            Pencil pencil = new Pencil(tipDurability, length, eraser);
            expectedInt = 6; //10 eraser, Test is 4 cost, 6 remainder

            pencil.Erase(singleWord5toWrite4toErase);
            actualInt = pencil.Eraser;

            Assert.AreEqual(expectedInt, actualInt);
        }


        [TestMethod]
        public void EraseErasesBackwardFromEndOfInputAndReturnsOnlyTheCharactersToBeErased()
        {
            int eraserWithTwoDurability = 2;
            Pencil pencil = new Pencil(tipDurability, length, eraserWithTwoDurability);
            expectedString = "st"; 
            // Test has 4 durability, 2 durability eraser will erase s and t from the end

            actualString = pencil.Erase(singleWord5toWrite4toErase);

            Assert.AreEqual(expectedString, actualString);
        }


        [TestMethod]
        public void EraseDoesNotLooseDurabilityOnWhiteSpace()
        {
            int eraserWith15Durability = 15; // More than enough durability to get through the testPhrase
            Pencil pencil = new Pencil(tipDurability, length, eraserWith15Durability);
            expectedInt = 3;

            pencil.Erase(phrase14toWrite12toErasewithWhitespace);
            actualInt = pencil.Eraser;

            Assert.AreEqual(expectedInt, actualInt);
            

        }

        /* Error Handling Tests */

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorThrowsExceptionOnPencilWithNegativeLength()
        {
            int negativeLength = -1;

            Pencil pencil = new Pencil(tipDurability, negativeLength);

            //Assert Is Exception Thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorThrowsExceptionOnPencilWithZeroOrNegTip()
        {
            int zeroTip = 0;

            Pencil pencil = new Pencil(zeroTip);

            //Assert is Exception Thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorThrowsExceptionOnPencilWithNegEraser()
        {
            int negativeEraser = -1;

            Pencil pencil = new Pencil(tipDurability,length,negativeEraser);

            //Assert is Exception Thrown
        }
    }
}
