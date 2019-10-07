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
            return word;
        }

        public int tipDruabilityLoss(string input)
        {
            return 5;
        }
    }


}
