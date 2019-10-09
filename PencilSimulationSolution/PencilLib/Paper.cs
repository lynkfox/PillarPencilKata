using System;
using System.Collections.Generic;
using System.Text;

namespace PencilLib
{
    public class Paper
    {
        /*Constants*/


        /*Internal Private Variables */
        private Queue<int> indexOfDeletes = new Queue<int>();
        private Queue<int> lengthOfDeletedWords = new Queue<int>();

        /*External Public Variables */

        public string Content { get; set; }

        /*Constructors*/
        public Paper()
        {

        }

        public void NewSheet()
        {
            this.Content = null;
            indexOfDeletes.Clear();
            lengthOfDeletedWords.Clear();
        }

        public void Prose(string writtenContent)
        {
            if(string.IsNullOrEmpty(this.Content) || this.Content == " ")
            {
                this.Content += writtenContent;
            }else
            {
                this.Content += " " + writtenContent;
            }

            
        }

        public void Delete(string wordToErase)
        {
            int wordLength = wordToErase.Length;
            string whiteSpaceReplace = "";
            int indexOfLastOccurance = this.Content.LastIndexOf(wordToErase);

            /*Generate enough whitespace to replace the word
             */
            for(int i=0; i< wordLength; i++)
            {
                whiteSpaceReplace += " ";
            }

            if(indexOfLastOccurance != -1) //-1 being returned if not found
            {
                this.Content = this.Content.Remove(indexOfLastOccurance, wordLength).Insert(indexOfLastOccurance, whiteSpaceReplace);
                indexOfDeletes.Enqueue(indexOfLastOccurance);
                lengthOfDeletedWords.Enqueue(wordLength);
            }
            
        }

        public void Edit(string replacementWord)
        {
            int replacementLength = replacementWord.Length;
            int lengthOfNextWhiteSpace = lengthOfDeletedWords.Dequeue();
            int startIndexOfDelete = indexOfDeletes.Dequeue();
            
            while(replacementLength < lengthOfNextWhiteSpace)
            {
                replacementWord += " ";
                replacementLength++;
            }

            if(replacementLength > lengthOfNextWhiteSpace)
            {
                int lengthOfDifference = replacementLength - lengthOfNextWhiteSpace;
                int indexOfNextSection = startIndexOfDelete + lengthOfNextWhiteSpace;

                string substringOfReplacementWord = replacementWord.Substring(replacementWord.Length-lengthOfDifference, lengthOfDifference);


                string substringToBeWrittenOver = this.Content.Substring(indexOfNextSection, lengthOfDifference);

                StringBuilder sb = new StringBuilder(this.Content);

                for (int i=0; i<lengthOfDifference; i++)
                {
                    if(char.IsWhiteSpace(substringToBeWrittenOver[i]))
                    {
                        sb[i + indexOfNextSection] = substringOfReplacementWord[i];
                        
                    }else
                    {
                        sb[i + indexOfNextSection] = '@';
                    }
                }
                this.Content = sb.ToString();
                replacementWord = replacementWord.Substring(0, lengthOfNextWhiteSpace);
            }
                
            this.Content = this.Content.Remove(startIndexOfDelete, lengthOfNextWhiteSpace).Insert(startIndexOfDelete, replacementWord);
        }
    }
}
