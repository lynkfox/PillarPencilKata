using System;

namespace PencilLib
{
    public class Pencil
    {
        /* CONST default vaules of Pencil
         */

        private const int LENGTH = 40;
        private const int TIP = 20;
        /* Private Internal variables
         */
        private int maxDurability;

        /* Public Variables
         */

        public int Tip { get; set; }
        public int Length { get; set; }




        public Pencil(int tipStartingValue)
        {
            maxDurability = tipStartingValue;
            this.Tip = maxDurability;
            this.Length = LENGTH;
        }

        public Pencil(int tipDurability, int length)
        {
            this.maxDurability= tipDurability;
            this.Length = length;
        }

        public string Write(string word)
        {
            this.Tip -= TipDurabilityLoss(word);
            return word;
        }

        public int TipDurabilityLoss(string input)
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
            this.Length -= 1;
            Tip = maxDurability;
        }
    }


}
