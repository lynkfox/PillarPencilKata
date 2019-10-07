using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilLib;

namespace PencilSimulationTests
{
    [TestClass]
    public class PaperTests
    {
        [TestMethod]
        public void TestPaperObjectCanContainContentOnIt()
        {
            string testInput = "This is a Test";
            Paper paper = new Paper();
            paper.Content = testInput;
            string expected = testInput;
            string actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestPaperProseAddsToCurrentContent()
        {
            string testInput = "This is a Test";
            Paper paper = new Paper();
            paper.Content = testInput;
            string expected = testInput + " " + testInput;

            paper.Prose(testInput);

            string actual = paper.Content;

            Assert.AreEqual(expected, actual);
            
        }

    }
}
