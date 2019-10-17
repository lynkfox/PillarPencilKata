using System;
using System.Collections.Generic;
using System.Text;

namespace PencilLib
{
    public class Paper
    {

        private Stack<int> indexOfDeletes = new Stack<int>();
        private Stack<int> lengthOfDeletedWords = new Stack<int>();


        private string Content { get; set; }


        public Paper()
        {

        }


        public void NewSheet()
        {
            this.Content = null;
            indexOfDeletes.Clear();
            lengthOfDeletedWords.Clear();
        }

        public string Read()
        {
            return this.Content;
        }


        public void Prose(string writtenContent)
        {
            //Trim exess white space from the end of the string
            if (!string.IsNullOrEmpty(writtenContent))
            {
                StringBuilder sb = new StringBuilder(writtenContent);
                int workingIndex = writtenContent.Length - 1;

                while (char.IsWhiteSpace(sb[workingIndex]))
                {
                    sb.Remove(workingIndex, 1);
                    workingIndex--;

                }
                writtenContent = sb.ToString();
            }

            //Making sure there is always a space between new content written, but not for first content
            if (string.IsNullOrEmpty(this.Content) || this.Content == " ")
            {
                this.Content += writtenContent;
            }
            else
            {
                this.Content += " " + writtenContent;
            }


        }

        public void Delete(string wordToErase)
        {

            if (!this.Content.Contains(wordToErase))
            {
                throw new Exception("There is no place on your paper that has \"" + wordToErase + "\" to be erased.");
            }

            int wordLength = wordToErase.Length;
            string whiteSpaceReplace;
            int indexOfLastOccurance = this.Content.LastIndexOf(wordToErase);


            whiteSpaceReplace = WhiteSpaceNeeded(wordLength);

            if (indexOfLastOccurance != -1) //-1 -> Word is not found with string.LastIndexOf
            {
                this.Content = this.Content.Remove(indexOfLastOccurance, wordLength).Insert(indexOfLastOccurance, whiteSpaceReplace);
                SaveWhiteSpaceOfLastDeletedWord(wordLength, indexOfLastOccurance);
            }

        }


        public void Edit(string replacementWord)
        {

            if (indexOfDeletes.Count == 0 || indexOfDeletes is null)
            {
                Prose(replacementWord);

            }
            else
            {
                int replacementLength = replacementWord.Length;
                int lengthOfNextWhiteSpace = lengthOfDeletedWords.Pop();
                int startIndexOfDelete = indexOfDeletes.Pop();

                if (replacementLength < lengthOfNextWhiteSpace)
                {
                    int whiteSpaceNeededToFillSameSpace = lengthOfNextWhiteSpace - replacementLength;
                    replacementWord += WhiteSpaceNeeded(whiteSpaceNeededToFillSameSpace);
                }



                StringBuilder contentSB = new StringBuilder(this.Content);

                for (int i = 0; i < replacementLength; i++)
                {
                    int currentIndexInContent = i + startIndexOfDelete;

                    if (i < lengthOfNextWhiteSpace)
                    {
                        contentSB[currentIndexInContent] = replacementWord[i];
                    }
                    else if (char.IsWhiteSpace(contentSB[currentIndexInContent]))
                    {
                        contentSB[currentIndexInContent] = replacementWord[i];
                    }
                    else //if letter is not whitespace, replace with :
                    {
                        contentSB[currentIndexInContent] = '@';
                    }
                }
                this.Content = contentSB.ToString();
            }


        }

        private void SaveWhiteSpaceOfLastDeletedWord(int wordLength, int indexOfLastOccurance)
        {
            indexOfDeletes.Push(indexOfLastOccurance);
            lengthOfDeletedWords.Push(wordLength);
        }

        private static string WhiteSpaceNeeded(int wordLength)
        {
            string whiteSpaceNeeded = "";
            for (int i = 0; i < wordLength; i++)
            {
                whiteSpaceNeeded += " ";
            }

            return whiteSpaceNeeded;
        }
    }
}
