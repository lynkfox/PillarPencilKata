﻿using System;

namespace PencilLib
{
    public class Pencil
    {
        /* Private Internal variables
         */
        private int maxDurability;
       

        /* Public Variables
         */

        public int Tip { get; set; }




        public Pencil(int tipStartingValue)
        {
            maxDurability = tipStartingValue;
            this.Tip = maxDurability;
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
            Tip = maxDurability;
        }
    }


}
