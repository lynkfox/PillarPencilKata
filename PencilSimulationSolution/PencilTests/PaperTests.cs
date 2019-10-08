using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilLib;

namespace PencilSimulationTests
{
    [TestClass]
    public class PaperTests
    {

        string testInput = "This is a Test";

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
            string wordToErase = "Test";
            string expected = "This is a     "; //1 whitespace space, 4 white space for T E S T

            paper.Delete(wordToErase);
            string actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestPaperDeleteRemovesLastInstanceInContentIfDuplicates()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            string duplicateWord = "Test";
            paper.Prose(duplicateWord);
            string wordToErase = "Test";
            string expected = "This is a Test     "; // 1 whitespace space, 4 white space for T E S T

            paper.Delete(wordToErase);
            string actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReturnSameStringIfWordToEraseIsNotInPaperContent()
        {
            Paper paper = new Paper();
            paper.Prose(testInput);
            string wordToErase = "NotHere";

            string expected = testInput;


            paper.Delete(wordToErase);
            string actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }


    }
}
