using System;

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
                    * As of this commit (10/7/19, late morning) I sent an email to request clarification (as one would do with a client in this situation)
                    * 
                    * Until then, continuing code by writting letter first (above) and then subtracting the durability. (so a Capital can still be written with 1 pt of durability)
                    * 
                    * If it it should return a blank space , then the add a letter will need to be adjusted to be AFTER the tip reduction, in its own if statement.
                    * 
                    * OR
                    * 
                    * the if check will need to check if the tip-the expected druabiliy loss will be less than 0.
                    */


                if (this.Tip > 0)
                {
                    outputPhrase += letter;

                    if (char.IsUpper(letter))
                    {

                        this.Tip -= 2;

                    }
                    else if (letter != ' ') //should covr all lower case and symbols.
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
         * TO DO : What if Length == 0?
         */
        public void Sharpen()
        {
            this.Length -= 1;
            Tip = maxDurability;
        }


        /* Function : Determines the Durability loss of input for Erasing a word based on the following criteria:
         * 
         * Letter (Capital or Lower) - 1pt
         * White Space - 0pt
         * All other Characters - 1pt
         *
         * To Do : Make Private (Not Needed outside of Pencil)
         */
        public int EraserDurabilityLoss(string input)
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

        public void Erase(string input)
        {
            this.Eraser = this.Eraser - EraserDurabilityLoss(input);
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
