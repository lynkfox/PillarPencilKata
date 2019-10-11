using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilLib;

namespace PencilSimulationTests
{
    [TestClass]
    public class PaperTests
    {

        string testInput = "This is a Test";
        string testInputMinusTest = "This is a     "; //1 whitespace space, 4 white space for T E S T
        string wordToErase= "Test";
        string eraseThis = "This";
        Paper paper = new Paper();

        string actual, expected;

        [TestMethod]
        public void ContentCanContainInformation()
        {

            paper.NewSheet();
            paper.Content = testInput;
            expected = testInput;

            actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        public void ProseAddsToCurrentContent()
        {


            paper.NewSheet();
            paper.Content = testInput;
            expected = testInput + " " + testInput;

            paper.Prose(testInput);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod]
        public void ProseProperlyAddsFirstProseWithoutWhitespace()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            expected = testInput;

            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteRemovesLastInstanceInContentAndLeavesWhiteSpace()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            expected = testInputMinusTest;

            paper.Delete(wordToErase);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void DeleteRemovesLastInstanceInContentIfDuplicates()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            string duplicateWord = wordToErase;
            paper.Prose(duplicateWord);
            expected = "This is a Test     ";

            paper.Delete(wordToErase);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void DeleteMultipleUsesContinueToLeaveWhiteSpace()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            expected = "     is a     ";

            paper.Delete(wordToErase);
            paper.Delete(eraseThis);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void EditAddsWordToFirstWhiteSpaceInQueue()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            paper.Delete(eraseThis);
            expected = "Word is a Test";

            paper.Edit("Word");
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeletesAndMultipleEditAtOnceReplaceInProperSpotsWithJustTwo()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            paper.Delete(wordToErase);
            paper.Delete(eraseThis);
            expected = "Word is a ABCD";

            paper.Edit("Word");
            paper.Edit("ABCD");
            actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void EditInWordThatIsSmallerThanWhiteSpaceAvailable()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            paper.Delete(eraseThis);
            expected = "A    is a Test";

            paper.Edit("A");
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EditWordIsLongerThanDeletedSpaceReplaceCharactersAsNeeded()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            paper.Delete(eraseThis);
            expected = "ABCDE@s a Test";

            paper.Edit("ABCDEF");
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EditFollowsDeleteInOrderOfLastToFirstDeleted()
        {
            paper.NewSheet();
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

            actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        public void ProseWillNotIncludeTrailingWhitespaceAddedToContent()
        {
            paper.NewSheet();
            string input = "Test Phrase With White    ";
            expected = "Test Phrase With White";

            paper.Prose(input);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProseAcceptsFrontLoadedWhiteSpaceAddedToContent()
        {
            paper.NewSheet();
            string input = "   Front Loaded White";
            expected = input;

            paper.Prose(input);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void EditWithNoWhiteSpaceAddsToEndOfContent()
        {
            paper.NewSheet();
            expected = testInput;

            paper.Edit(testInput);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);

            
        }


        [TestMethod]
        public void DeleteNotFindingValueOfDeleteReturnsExceptionMessageForDisplay()
        {
            paper.NewSheet();
            string notInInput = "ABCDE";
            paper.Prose(testInput);
            expected = "There is no place on your paper that has \"ABCDE\" to be erased.";

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
            paper.NewSheet();
            paper.Prose(testInput);
            expected = paper.Content;
            string wordNotInContent = "ABCDE";

            try
            {
                paper.Delete(wordNotInContent);
            }catch
            {
                //Doing nothing with exception;
            }
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProseWhenGivenNothingProperlyDoesNothingWrong()
        {
            paper.NewSheet();
            expected = "";
            string nullString = null;

            paper.Prose(nullString);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void DeleteWhenGivenSomeCharactersOfAWordDeletesThemAppropriately()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            string notFullWordDelete = "es";
            expected = "This is a T  t";

            paper.Delete(notFullWordDelete);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }


    }
}
