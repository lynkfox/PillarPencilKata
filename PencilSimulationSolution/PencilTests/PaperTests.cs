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
        Paper paper = new Paper();

        string actual, expected;

        private void CleanPaper()
        {
            paper.Content = null; //Make sure paper has nothing on it
        }

        [TestMethod]
        public void TestPaperObjectCanContainContentOnIt()
        {

            CleanPaper();
            paper.Content = testInput;
            expected = testInput;
            actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestPaperProseAddsToCurrentContent()
        {


            CleanPaper();

            paper.Content = testInput;
            expected = testInput + " " + testInput;

            paper.Prose(testInput);

            actual = paper.Content;

            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod]
        public void TestPaperProseProperlyAddsFirstProseWithoutWhitespace()
        {
            CleanPaper();
            paper.Prose(testInput);
            expected = testInput;
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestPaperDeleteRemovesLastInstanceInContentAndLeavesWhiteSpace()
        {
            CleanPaper();
            paper.Prose(testInput);
            expected = testInputMinusTest;

            paper.Delete(wordToErase);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestPaperDeleteRemovesLastInstanceInContentIfDuplicates()
        {
            CleanPaper();
            paper.Prose(testInput);
            string duplicateWord = wordToErase;
            paper.Prose(duplicateWord);
            expected = "This is a Test     ";

            paper.Delete(wordToErase);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReturnSameStringIfWordToEraseIsNotInPaperContent()
        {
            CleanPaper();
            paper.Prose(testInput);
            string wordToEraseNotInContent = "NotHere";

            expected = testInput;


            paper.Delete(wordToEraseNotInContent);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MultipleUsesOfDeleteContinueToLeaveWhiteSpace()
        {
            CleanPaper();
            paper.Prose(testInput);
            string secondToErase = "This";

             expected = "     is a     ";

            paper.Delete(wordToErase);
            paper.Delete(secondToErase);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }


    }
}
