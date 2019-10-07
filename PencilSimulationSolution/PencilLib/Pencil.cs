using System;

namespace PencilLib
{
    public class Pencil
    {
        /* Private Internal variables
         */
       

        /* Public Variables
         */

        public int tip { get; set; }




        public Pencil(int tipStartingValue)
        {
            this.tip = tipStartingValue;
        }

        public string write(string word)
        {
            this.tip -= tipDruabilityLoss(word);
            return word;
        }

        public int tipDruabilityLoss(string input)
        {
            int totalDurabilityCost = 0;
            foreach(char letter in input)
            {
                if(char.IsUpper(letter))
                {
                    //Capital Letters increase durability cost by 1
                    totalDurabilityCost += 2;
                }else if (letter != ' ' )
                {
                    // do nothing if it is a white space, but otherwise increase durability cost by 1
                    totalDurabilityCost++;
                }
            }

            return totalDurabilityCost;
        }

        public void Sharpen()
        {
            tip = 20;
        }
    }


}
