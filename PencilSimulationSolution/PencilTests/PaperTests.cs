using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilLib;

namespace PencilSimulationTests
{
    [TestClass]
    public class PaperTests
    {

        public string testInput = "This is a Test";
        public string testInputMinusTest = "This is a     "; //1 whitespace space, 4 white space for T E S T
        public string wordToErase = "Test";
        public string eraseThis = "This";
        public Paper paper = new Paper();

        public string actual, expected;

        [TestMethod]
        public void ReadReturnsContentOfPaper()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            expected = testInput;

            actual = paper.Read();

            Assert.AreEqual(expected, actual);
        }




        [TestMethod]
        public void ProseAddsToCurrentContent()
        {


            Paper paper = new Paper();
            paper.Prose(testInput);
            expected = testInput + " " + testInput;

            paper.Prose(testInput);
            actual = paper.Read();

            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod]
        public void ProseProperlyAddsFirstProseWithoutWhitespace()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            expected = testInput;

            actual = paper.Read();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteRemovesLastInstanceInContentAndLeavesWhiteSpace()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            expected = testInputMinusTest;

            paper.Delete(wordToErase);
            actual = paper.Read();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void DeleteRemovesLastInstanceInContentIfDuplicates()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            string duplicateWord = wordToErase;
            paper.Prose(duplicateWord);
            expected = "This is a Test     ";

            paper.Delete(wordToErase);
            actual = paper.Read();

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void DeleteMultipleUsesContinueToLeaveWhiteSpace()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            expected = "     is a     ";

            paper.Delete(wordToErase);
            paper.Delete(eraseThis);
            actual = paper.Read();

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void EditAddsWordToFirstWhiteSpaceInQueue()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            paper.Delete(eraseThis);
            expected = "Word is a Test";

            paper.Edit("Word");
            actual = paper.Read();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeletesAndMultipleEditAtOnceReplaceInProperSpotsWithJustTwo()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            paper.Delete(wordToErase);
            paper.Delete(eraseThis);
            string word = "Word";
            string ABCD = "ABCD";
            expected = "Word is a ABCD";

            paper.Edit(word);
            paper.Edit(ABCD);
            actual = paper.Read();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void EditInWordThatIsSmallerThanWhiteSpaceAvailable()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            paper.Delete(eraseThis);
            string singleCharacterToEditIn = "A";
            expected = "A    is a Test";

            paper.Edit(singleCharacterToEditIn);
            actual = paper.Read();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EditWordIsLongerThanDeletedSpaceReplaceCharactersAsNeeded()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            paper.Delete(eraseThis);
            string wordToEditIn = "ABCDEF";
            expected = "ABCDE@s a Test";

            paper.Edit(wordToEditIn);
            actual = paper.Read();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EditFollowsDeleteInOrderOfLastToFirstDeleted()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            string firstDelete = "is";
            string secondDelete = "Test";
            string thirdDelete = "This";
            paper.Delete(firstDelete);
            paper.Delete(secondDelete);
            paper.Delete(thirdDelete);

            paper.Edit(firstDelete);
            paper.Edit(secondDelete);
            paper.Edit(thirdDelete);

            expected = "is   Thi@ Test";

            actual = paper.Read();

            Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        public void ProseWillNotIncludeTrailingWhitespaceAddedToContent()
        {
            Paper paper = new Paper();
            string input = "Test Phrase With White    ";
            expected = "Test Phrase With White";

            paper.Prose(input);
            actual = paper.Read();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProseAcceptsFrontLoadedWhiteSpaceAddedToContent()
        {
            Paper paper = new Paper();
            string input = "   Front Loaded White";
            expected = input;

            paper.Prose(input);
            actual = paper.Read();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void EditWithNoWhiteSpaceAddsToEndOfContent()
        {
            Paper paper = new Paper();
            expected = testInput;

            paper.Edit(testInput);
            actual = paper.Read();

            Assert.AreEqual(expected, actual);

            
        }


        [TestMethod]
        public void DeleteNotFindingValueOfDeleteReturnsExceptionMessageForDisplay()
        {
            Paper paper = new Paper();
            string notInInput = "ABCDE";
            paper.Prose(testInput);
            expected = "There is no place on your paper that has \""+ notInInput+"\" to be erased.";

            try
            {
                paper.Delete(notInInput);
            }catch(Exception e)
            {
                actual = e.Message;
            }

            Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        public void DeleteIfItDoesNotFindWordDoesNotModifyContent()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            expected = paper.Read();
            string wordNotInContent = "ABCDE";

            try
            {
                paper.Delete(wordNotInContent);
            }catch
            {
                //Doing nothing with exception;
            }
            actual = paper.Read();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProseWhenGivenNothingProperlyDoesNothingWrong()
        {
            Paper paper = new Paper();
            expected = "";
            string nullString = null;

            paper.Prose(nullString);
            actual = paper.Read();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void DeleteWhenGivenSomeCharactersOfAWordDeletesThemAppropriately()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            string notFullWordDelete = "es";
            expected = "This is a T  t";

            paper.Delete(notFullWordDelete);
            actual = paper.Read();

            Assert.AreEqual(expected, actual);
        }


    }
}
