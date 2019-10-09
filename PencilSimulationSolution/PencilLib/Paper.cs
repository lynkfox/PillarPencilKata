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
                if(i< lengthOfNextWhiteSpace)
                {
                    sb[startIndexOfDelete + i] = replacementWord[i];
                }else if(char.IsWhiteSpace(sb[startIndexOfDelete + i]))
                {
                    sb[i + startIndexOfDelete] = replacementWord[i];
                        
                }else
                {
                    sb[i + startIndexOfDelete] = '@';
                }
            }
            this.Content = sb.ToString();
                
            
                
            
        }
    }
}
