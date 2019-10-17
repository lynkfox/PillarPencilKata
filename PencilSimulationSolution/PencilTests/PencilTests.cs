using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilLib;
using System;

namespace PencilSimulationTests
{
    [TestClass]
    public class PencilTests
    {
        public int length500 = 500;
        public int tipDurability20 = 20;
        public int tipDurability1 = 1;
        public int eraserDurability10 = 10;

        public string oneDurabilityWrite = "a";

        public string singleWord5toWrite4toErase = "Test";
        public int tipDurabilityAfterSingle15 = 15;
        public int eraserDurabilityAfterSingle6 = 6;

        public string phrase14toWrite12toErasewithWhitespace = "This is Phrase";
        public int tipDurabilityAfterPhrase6 = 6;
        public int eraserDurabilityAfterPhrase0 = 0;

        int expectedInt, actualInt;
        string expectedString, actualString;


        /* Pencil Tip Tests
         */

        [TestMethod]
        public void TipCanBeRetrievedWithCheckTip()
        {
            Pencil pencil = new Pencil(tipDurability20);
            expectedInt = tipDurability20;

            actualInt = pencil.CheckTip();

            Assert.AreEqual(expectedInt, actualInt);
        }

        
        [TestMethod]
        public void WriteWordReducesTipDurability()
        {
            Pencil pencil = new Pencil(tipDurability20, length500);
            expectedInt = tipDurabilityAfterPhrase6;
            pencil.Write(phrase14toWrite12toErasewithWhitespace);
            actualInt = pencil.CheckTip();

            Assert.AreEqual(expectedInt, actualInt);
        }


       
        /* Pencil Write Tests */

   
        [TestMethod]
        public void WriteReturnsString()
        {
            Pencil pencil = new Pencil(tipDurability20, length500);
            expectedString = singleWord5toWrite4toErase;

            actualString = pencil.Write(singleWord5toWrite4toErase);
            
            Assert.AreEqual(expectedString, actualString);

        }


        [TestMethod]
        public void WriteStopsAt0Durabilility()
        {
            Pencil pencil = new Pencil(tipDurability20, length500, eraserDurability10);
            expectedInt = 0;

            
            pencil.Write(phrase14toWrite12toErasewithWhitespace);  
            pencil.Write(phrase14toWrite12toErasewithWhitespace); 
            actualInt = pencil.CheckTip();

            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void WriteReturnsWhiteSpaceifDurabilityRunsOut()
        {
            int lowDurability = 2;
            Pencil pencilLowTip = new Pencil(lowDurability);
            expectedString = "T   ";

            actualString = pencilLowTip.Write(singleWord5toWrite4toErase);
            
            Assert.AreEqual(expectedString, actualString);
        }


        [TestMethod]
        public void WriteAdjustsDurabilityCorrectilyForWhiteSpaceWithMoreLettersThanDurabilityLeft()
        {
            Pencil pencil = new Pencil(tipDurability20);
            string longPhrase = "This Phrase Is More Than Twenty Durability"; //43 total durability
            expectedString = "This Phrase Is More                       "; //20 written durability, 26 white spaces (3 no durability white spaces)
            int expectedTip = 0;

            actualString = pencil.Write(longPhrase);
            int actualTip = pencil.CheckTip();

            Assert.AreEqual(expectedString, actualString);
            Assert.AreEqual(expectedTip, actualTip);
        }

        [TestMethod]
        public void WriteWithZeroDurabilityReturnsProperNumberOfWhiteSpace()
        {
            Pencil pencil = new Pencil(tipDurability1);
            pencil.Write(oneDurabilityWrite);
            expectedString = "    "; //T E S T - 4 letters, 4 white space

            actualString = pencil.Write(singleWord5toWrite4toErase);

            Assert.AreEqual(expectedString, actualString);



        }

        [TestMethod]
        public void WriteCapitalLetterWithOnePointDurabilityReturnsCapitalLetterAnd0Durability()
        {
            Pencil pencil = new Pencil(1);
            expectedString = "T";
            expectedInt = 0;

            

            actualString = pencil.Write("T");
            actualInt = pencil.CheckTip();

            Assert.AreEqual(expectedInt, actualInt);
            Assert.AreEqual(expectedString, actualString);



        }


        /* Pencil Sharpen  Tests */

        [TestMethod]
        public void SharpenReturnsToMaxValue()
        {
            Pencil pencil = new Pencil(tipDurability20, length500);
            expectedInt = tipDurability20; 

            pencil.Sharpen();
            actualInt = pencil.CheckTip();

            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void SharpenReturnsToMaxValueWithRangeOfDurabilityAfterWrite()
        {
            for (expectedInt = 1; expectedInt < 99999; expectedInt += 11)
            {
                SharpenOneValue(expectedInt);
            }

        }

        private void SharpenOneValue(int pencilTip)
        {
            Pencil pencil = new Pencil(pencilTip);
            pencil.Write(phrase14toWrite12toErasewithWhitespace);
            pencil.Sharpen();
            actualInt = pencil.CheckTip();

            Assert.AreEqual(pencilTip, actualInt);

        }

        [TestMethod]
        public void LengthCanBeCheckedWithGetLength()
        {
            Pencil pencil = new Pencil(tipDurability20, length500);
            expectedInt = length500;

            actualInt = pencil.GetLength();

            Assert.AreEqual(expectedInt, actualInt);
        }

        [TestMethod]
        public void SharpenReducesPencilLengthBy1()
        {
            
            Pencil lengthPencil = new Pencil(tipDurability20, length500);
            expectedInt = length500 - 1;

            lengthPencil.Sharpen();
            actualInt = lengthPencil.GetLength();
            

            Assert.AreEqual(expectedInt, actualInt);
        }


        [TestMethod]
        public void SharpenPencilWithZeroLengthDoesNotReturnTipToMaximum()
        {
            int zeroPencilLength = 0; 
            
            Pencil pencil = new Pencil(tipDurability20, zeroPencilLength, eraserDurability10);
            pencil.Write(singleWord5toWrite4toErase); 
            expectedInt = tipDurabilityAfterSingle15;

            pencil.Sharpen();

            actualInt = pencil.CheckTip();

            Assert.AreEqual(expectedInt, actualInt);
        }




        /* Pencil Eraser Tests*/

  
        [TestMethod]
        public void EraserReturnsDurability()
        {
            
            Pencil pencil = new Pencil(tipDurability20, length500, eraserDurability10);
            expectedInt = eraserDurability10;

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
            Pencil pencil = new Pencil(tipDurability20, length500, zeroEraserDurability);
            expectedString = "";

            actualString = pencil.Erase(singleWord5toWrite4toErase);

            Assert.AreEqual(expectedString, actualString);

        }


        [TestMethod]
        public void EraseProperlyRemovesDurabilityOfErasedItem()
        {
            Pencil pencil = new Pencil(tipDurability20, length500, eraserDurability10);
            expectedInt = eraserDurabilityAfterSingle6;

            pencil.Erase(singleWord5toWrite4toErase);
            actualInt = pencil.Eraser;

            Assert.AreEqual(expectedInt, actualInt);
        }


        [TestMethod]
        public void EraseErasesBackwardFromEndOfInputAndReturnsOnlyTheCharactersToBeErased()
        {
            int eraserWithTwoDurability = 2;
            Pencil pencil = new Pencil(tipDurability20, length500, eraserWithTwoDurability);
            expectedString = "st"; 
            // Test has 4 durability, 2 durability eraser will erase s and t from the end

            actualString = pencil.Erase(singleWord5toWrite4toErase);

            Assert.AreEqual(expectedString, actualString);
        }


        [TestMethod]
        public void EraseDoesNotLooseDurabilityOnWhiteSpace()
        {
            int eraserWith15Durability = 15; 
            Pencil pencil = new Pencil(tipDurability20, length500, eraserWith15Durability);
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

            Pencil pencil = new Pencil(tipDurability20, negativeLength);

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

            Pencil pencil = new Pencil(tipDurability20,length500,negativeEraser);

            //Assert is Exception Thrown
        }
    }
}
