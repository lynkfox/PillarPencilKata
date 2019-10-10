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
        public void EraseReturnSameStringIfNotInPaperContent()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            string wordToEraseNotInContent = "NotHere";

            expected = testInput;


            paper.Delete(wordToEraseNotInContent);
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
        public void EditAddsWordToFirstWhiteSpaceInList()
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
            paper.Delete(eraseThis);
            paper.Delete(wordToErase);

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

        
    }
}
