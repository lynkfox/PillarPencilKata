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
         *
         * 
         * This test is now Deprecated slightly, because of ErrorHandling being added. New test to follow.

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
        */

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


        /*This test is for the combination of Pencil.Write and Paper.Edit - 
         * 
         * pencil.write will return white space if there is not enough durability. But no one would want
         * to leave that blank white space there, so if a phrase comes in with any whitespace at the end,
         * we want to ensure that it does not enter  paper.content
         */
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

        /*Error Handling Tests*/
        /* If there is no white space in the middle of content, then simply add the Edit to to the
         * end of Content
         */
        [TestMethod]
        public void EditWithNoWhiteSpaceAddsToEndOfContent()
        {
            paper.NewSheet();
            expected = testInput;

            paper.Edit(testInput);

            actual = paper.Content;

            Assert.AreEqual(expected, actual);

            
        }


        /* Testing to make sure the proper exception message is thrown here.
         */
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

        /*New test to repeat what depreciated test used to test for
         * 
         */

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
