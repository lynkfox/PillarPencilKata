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
