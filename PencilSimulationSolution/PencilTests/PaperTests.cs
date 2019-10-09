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

        [TestMethod]
        public void TestPaperObjectCanContainContentOnIt()
        {
            
            Paper paper = new Paper();
            paper.Content = testInput;
            string expected = testInput;
            string actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestPaperProseAddsToCurrentContent()
        {
            
            Paper paper = new Paper();
            paper.Content = testInput;
            string expected = testInput + " " + testInput;

            paper.Prose(testInput);

            string actual = paper.Content;

            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod]
        public void TestPaperProseProperlyAddsFirstProseWithoutWhitespace()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            string expected = testInput;
            string actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestPaperDeleteRemovesLastInstanceInContentAndLeavesWhiteSpace()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            string expected = testInputMinusTest;

            paper.Delete(wordToErase);
            string actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestPaperDeleteRemovesLastInstanceInContentIfDuplicates()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            string duplicateWord = wordToErase;
            paper.Prose(duplicateWord);
            string expected = "This is a Test     ";

            paper.Delete(wordToErase);
            string actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReturnSameStringIfWordToEraseIsNotInPaperContent()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            string wordToEraseNotInContent = "NotHere";

            string expected = testInput;


            paper.Delete(wordToEraseNotInContent);
            string actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MultipleUsesOfDeleteContinueToLeaveWhiteSpace()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            string wordToErase = "Test";
            string secondToErase = "This";

            string expected = "     is a     ";

            paper.Delete(wordToErase);
            paper.Delete(secondToErase);
            string actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }


    }
}
