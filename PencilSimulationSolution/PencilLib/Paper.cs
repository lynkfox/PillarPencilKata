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
            this.Content = "";
        }


        public void NewSheet()
        {
            this.Content = "";
            indexOfDeletes.Clear();
            lengthOfDeletedWords.Clear();
        }

        public string Read()
        {
            return this.Content;
        }


        public void Prose(string writtenContent)
        {
            if(!string.IsNullOrEmpty(writtenContent))
            {
                string trimedToWrite = TrimExtraWhiteSpaceFromEndOfInputToWrite(writtenContent);

                AddInputToContent(trimedToWrite);
            }
        }

        private string TrimExtraWhiteSpaceFromEndOfInputToWrite(string input)
        {
           
            StringBuilder sb = new StringBuilder(input);
            int workingIndex = input.Length - 1;

            while (char.IsWhiteSpace(sb[workingIndex]))
            {
                sb.Remove(workingIndex, 1);
                workingIndex--;

            }
            return sb.ToString();
            
        }

        private void AddInputToContent(string input)
        {
            if (string.IsNullOrEmpty(this.Content) || this.Content == " ")
            {
                this.Content += input;
            }
            else
            {
                this.Content += " " + input;
            }
        }



        public void Delete(string wordToErase)
        {

            ThrowExceptionIfWordNotFound(wordToErase);

            int wordLength = wordToErase.Length;
            int indexOfLastOccurance = this.Content.LastIndexOf(wordToErase);
            

            if (indexOfLastOccurance != -1) //-1 -> Word is not found with string.LastIndexOf
            {
                RemoveWordAndAddWhiteSpace(wordLength, indexOfLastOccurance);
                SaveWhiteSpaceOfLastDeletedWord(wordLength, indexOfLastOccurance);
            }

        }

        private void RemoveWordAndAddWhiteSpace(int length, int index)
        {
            
            string whiteSpaceReplace = WhiteSpaceNeeded(length);
            this.Content = this.Content.Remove(index, length).Insert(index, whiteSpaceReplace);;
            
        }

        private void ThrowExceptionIfWordNotFound(string word)
        {
            if (!this.Content.Contains(word))
            {
                throw new Exception("There is no place on your paper that has \"" + word + "\" to be erased.");
            }
        }




        public void Edit(string replacementString)
        {

            if (indexOfDeletes.Count == 0 || indexOfDeletes is null)
            {
                Prose(replacementString);
            }
            else
            {
                int lengthOfNextWhiteSpace = lengthOfDeletedWords.Peek();

                replacementString = AddWhiteSpaceToReplacementWordIfNeeded(replacementString, lengthOfNextWhiteSpace);

                this.Content = AddEditToLastWhiteSpaceInContent(replacementString);

            }
        }

        private string AddWhiteSpaceToReplacementWordIfNeeded(string replacement, int lengthNeeded)
        {
            if (replacement.Length < lengthNeeded)
            {
                int whiteSpaceNeededToFillSameSpace = lengthNeeded - replacement.Length;
                replacement += WhiteSpaceNeeded(whiteSpaceNeededToFillSameSpace);
            }

            return replacement;
        }

        private string AddEditToLastWhiteSpaceInContent(string replacement)
        {
            int replacementLength = replacement.Length;
            int lengthOfNextWhiteSpace = lengthOfDeletedWords.Pop();
            int startIndexOfDelete = indexOfDeletes.Pop();
            StringBuilder contentSB = new StringBuilder(this.Content);

            for (int i = 0; i < replacementLength; i++)
            {
                int currentIndexInContent = i + startIndexOfDelete;

                if (i < lengthOfNextWhiteSpace)
                {
                    contentSB[currentIndexInContent] = replacement[i];
                }
                else if (char.IsWhiteSpace(contentSB[currentIndexInContent]))
                {
                    contentSB[currentIndexInContent] = replacement[i];
                }
                else //if letter is not whitespace, replace with :
                {
                    contentSB[currentIndexInContent] = '@';
                }
            }
            return contentSB.ToString();
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
