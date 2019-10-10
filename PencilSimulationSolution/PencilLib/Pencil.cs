using System;
using System.Text;

namespace PencilLib
{
    public class Pencil
    {
        /* CONST default vaules of Pencil
         */
        private const int LENGTH = 40;
        private const int TIP = 20;
        private const int ERASER = 60;



        /* Private Internal variables
         */
        private int maxDurability;



        /* Public Variables
         */
        public int Tip { get; set; }
        public int Length { get; set; }
        public int Eraser { get; set; }


        /* Constructors */
        public Pencil() : this(TIP, LENGTH, ERASER)
        {
        }

        public Pencil(int tipStartingValue) : this(tipStartingValue, LENGTH, ERASER)
        {
        }

        public Pencil(int tipDurability, int length) : this(tipDurability,length, ERASER)
        {
        }

        public Pencil(int tipDurability, int length, int eraserDurability)
        {
            this.maxDurability = tipDurability;
            this.Tip = this.maxDurability;
            this.Length = length;
            this.Eraser = eraserDurability;
        }


        /* Function : Reduces the Tip Durability by the cost of the input. 
         * 
         * Returns the word for use later.
         * 
         * If the tip does not have enough durability, return a word that has enough white space for each letter.
         */
        public string Write(string word)
        {
            string outputPhrase = "";

            foreach (char letter in word)
            {

                /* Requirements for Kata do not state what to do if the letter is a Capital (2 points loss)
                    * and there is only 1 point of durability lost. 
                    * 
                    * After Discussing this with the "Client" it was returned to me to "Do what you think is best"
                    * 
                    * So the letter will be written, even if it costs more than the available points
                    * 
                    * Reasoning: better to fail upward - That is, better to produce expected results then leave the
                    * user wondering why it did not work.
                    */


                if (this.Tip > 0)
                {
                    outputPhrase += letter;

                    if (char.IsUpper(letter))
                    {

                        this.Tip -= 2;

                    }
                    else if (letter != ' ') //should cover all lower case and symbols.
                    {

                        this.Tip -= 1;
                    }
                }else
                {
                    outputPhrase += " ";
                }
                

                
            }
            
            
            /*This is a nice little safety valve, due to current understanding of requirements.
             * 
             * if there is a capital letter (2pts) attempted to be wrote with just 1 durability pt left, it 
             * currently still writes, and will be at -1 durability afterward. This fixes that.
             */

            if (this.Tip < 0)
            {
                this.Tip = 0;
            }

            
            return outputPhrase;
        }


        /* Function - Returns Pencil to max Sharpness while reducing Length.
         * 
         * If Length == 0 this function simply does not sharpen the pencil anymore.
         * 
         * This can be assumed to be a nub of a pencil, that is just Tip+Eraser with nothing left
         * to give if sharpening continues. Once this pencil runs out of durability there is no
         * choice but to "purchase an additional pencil"
         */
        public void Sharpen()
        {
            if(this.Length > 0)
            {
                this.Length -= 1;
                Tip = maxDurability;
            }

            
        }


        /* Depreciated function
         * 
         * Function : Determines the Durability loss of input for Erasing a word based on the following criteria:
         * 
         * Letter (Capital or Lower) - 1pt
         * White Space - 0pt
         * All other Characters - 1pt
         *
         
        private int EraserDurabilityLoss(string input)
        {
            int durabilityPoints = 0;
            foreach(char letter in input)
            {
                if (letter != ' ')
                {
                    durabilityPoints++;
                }
            }
            return durabilityPoints;
        }
        */


        public string Erase(string erasedWord)
        {
            StringBuilder sb = new StringBuilder(erasedWord);
            int erasedLength = erasedWord.Length;

            if (this.Eraser > 0)
            {
                for(int i = erasedLength; i>0; i--)
                {
                    if(this.Eraser == 0)
                    {
                        sb.Remove(0, i);
                        i = 0;
                    }else
                    {
                        this.Eraser--;
                    }
                }
                return sb.ToString(); ;


            } else
            {
                return "";
            }
            
        }
    }


    /* DEPRECIATED - No longer needed 
     * 
     * (working it into .Write() was more effecient for determining white space after tip is gone)
     * 
     * Function: Determines the Durability Cost of Writing using the following requriements:
     * 
     * Capital Letter - 2pts
     * Lower Case Letter - 1pt
     * White Space = 0pts
     * 
     * Other Characters = 1pt;
     * 
     *
    public int TipDurabilityLoss(string input)
    {
        int totalDurabilityCost = 0;
        foreach(char letter in input)
        {
            if(char.IsUpper(letter))
            {
                //Capital Letters increase durability cost by 2
                totalDurabilityCost += 2;
            }else if (letter != ' ' )
            {
                // do nothing if it is a white space, but otherwise increase durability cost by 1
                totalDurabilityCost++;
            }
        }

        return totalDurabilityCost;
    }

        */
}
