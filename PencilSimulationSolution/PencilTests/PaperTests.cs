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

      
        /*Test to make sure the paper remembers what is written on it
         */

        [TestMethod]
        public void ContentCanContainInformation()
        {

            paper.NewSheet();
            paper.Content = testInput;
            expected = testInput;
            actual = paper.Content;

            Assert.AreEqual(expected, actual);

        }

        /* Test to make sure Prose properly adds content to the Paper, with proper white space between content
         */
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

        /*Make sure that the first content doesn't have an extra whitespace starting
         */
        [TestMethod]
        public void ProseProperlyAddsFirstProseWithoutWhitespace()
        {
            paper.NewSheet();
            paper.Prose(testInput);
            expected = testInput;
            actual = paper.Content;

            Assert.AreEqual(expected, actual);
        }

        /*Test that delete is removing words from the phrase and leaves a proper amount of white space
         */
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

        /*Test to make sure that if there is multiple instance of the phrase to delete it only finds
         * the last one
         */
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


        /*Determine if the phrase to be Erased doesn't exist that the content isn't affected
         */

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

        /*Test that delete continues to go back and leaves white spaces as needed
         */
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


        /* Using a Queue so make sure that Edit adds to the last thing deleted in instance of 2 deletes
         */
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

        /* Continue testing of Edit - same test as above basically
         */
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

        /* making sure that there is still proper white space left if edit is added
         */
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

        /* test if there is a longer word than the white space available
         */
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

        /* Edit Method needs to follow Last In First Out as it makes the most sense, so make sure
         * with multiple deletes that it does
         */
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

        
    }
}
