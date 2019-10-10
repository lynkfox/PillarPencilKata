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


        /* This function sets the Paper to a New Sheet. 
         * 
         * Kind of a misnomer, but if we want to save the paper working on should be done in a
         * new object
         */
        public void NewSheet()
        {
            this.Content = null;
            indexOfDeletes.Clear();
            lengthOfDeletedWords.Clear();
        }

        /*This function adds new content to the Page, at the end of existing content
         */
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

        /* This function removes the last instance of the input from the current content, leaving
         * a white space behind of the same length.
         * 
         * it then adds that length of white space and the location of it in the content to a 
         * set of queues to be found later for edit
         */
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


        /* Edit follows the Queue philosophy of editing.
         * 
         * It fills in the Last White Space created, and moving back in order they are created
         * 
         */
        public void Edit(string replacementWord)
        {
            int replacementLength = replacementWord.Length;
            int lengthOfNextWhiteSpace = lengthOfDeletedWords.Dequeue();
            int startIndexOfDelete = indexOfDeletes.Dequeue();
            
            /*add White space to fill if the replacement word is less than what
             * is recorded of the last deleted space
             */
            while(replacementLength<lengthOfNextWhiteSpace)
            {
                replacementWord += " ";
                replacementLength++;
            }

                
            StringBuilder sb = new StringBuilder(this.Content);

            for (int i=0; i<replacementLength; i++)
            {
                int currentIndexInProcess = i + startIndexOfDelete;

                if(i< lengthOfNextWhiteSpace)
                {
                    sb[currentIndexInProcess] = replacementWord[i];
                }
                else if(char.IsWhiteSpace(sb[currentIndexInProcess]))
                {
                    sb[currentIndexInProcess] = replacementWord[i];
                        
                }
                else
                {
                    sb[currentIndexInProcess] = '@';
                }
            }
            this.Content = sb.ToString();
                
            
                
            
        }
    }
}
