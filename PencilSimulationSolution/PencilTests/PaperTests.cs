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



    }
}
