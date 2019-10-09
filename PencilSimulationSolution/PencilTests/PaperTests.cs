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

      

        [TestMethod]
        public void TestPaperObjectCanContainContentOnIt()
        {

            paper.NewSheet();
            paper.Content = testInput;
            expected = testInput;
            actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestPaperProseAddsToCurrentContent()
        {


            paper.NewSheet();

            paper.Content = testInput;
            expected = testInput + " " + testInput;

            paper.Prose(testInput);

            actual = paper.Content;

            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod]
        public void TestPaperProseProperlyAddsFirstProseWithoutWhitespace()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            expected = testInput;
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestPaperDeleteRemovesLastInstanceInContentAndLeavesWhiteSpace()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            expected = testInputMinusTest;

            paper.Delete(wordToErase);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestPaperDeleteRemovesLastInstanceInContentIfDuplicates()
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
        public void ReturnSameStringIfWordToEraseIsNotInPaperContent()
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
        public void MultipleUsesOfDeleteContinueToLeaveWhiteSpace()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            string secondToErase = "This";

             expected = "     is a     ";

            paper.Delete(wordToErase);
            paper.Delete(secondToErase);
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestThatPaperEditAddsWordToFirstWhiteSpaceInList()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            string eraseThis = "This";
            paper.Delete(eraseThis);

            expected = "Word is a Test";

            paper.Edit("Word");

            actual = paper.Content;
            Assert.AreEqual(expected, actual);
        }



        

    }
}
