using System;
using System.Text;

namespace PencilLib
{
    public class Pencil
    {
        /* default vaules of Pencil
         */
        private const int LENGTH = 40;
        private const int TIP = 20;
        private const int ERASER = 60;


        private int maxDurability;



        /* In Production these should really be ReadOnly, but I've left them public for the test classes
         * at this time.
         */
        public int Tip { get; set; }
        public int Length { get; set; }
        public int Eraser { get; set; }


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
            if (tipDurability <= 0)
            {
                throw new ArgumentOutOfRangeException("PencilTipLessThan1", "Pencil Cannot Have Zero or Negative Tip");
            }
            else
            {
                this.maxDurability = tipDurability;
                this.Tip = this.maxDurability;
            }

            
            if(length <0)
            {
                throw new ArgumentOutOfRangeException("PencilLengthNegative","Pencil Cannot Have Negitive Length");
            }else
            {
                this.Length = length;
            }

            if (eraserDurability < 0)
            {
                throw new ArgumentOutOfRangeException("PencilEraserNegative", "Pencil Cannot Have Negative Eraser");
            }
            else
            {
                this.Eraser = eraserDurability;
            }

            
        }


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
                    ReduceTipDurability(letter);
                }
                else
                {
                    outputPhrase += " ";
                }
                

                
            }
            
            if (this.Tip < 0)
            {
                this.Tip = 0;
            }

            
            return outputPhrase;
        }

      

        public void Sharpen()
        {
            if(this.Length > 0)
            {
                this.Length -= 1;
                Tip = maxDurability;
            }
        }

        public string Erase(string erasedWord)
        {
            
            

            if (this.Eraser > 0)
            {
                
                return CharactersToBeErased(erasedWord);

            }
            else
            {
                return "";
            }
            
        }

        private string CharactersToBeErased(string erasedWord)
        {
            StringBuilder sb = new StringBuilder(erasedWord);
            int erasedLength = erasedWord.Length;
            for (int i = erasedLength; i > 0; i--)
            {
                if (this.Eraser == 0)
                {
                    sb.Remove(0, i);
                    i = 0;
                }
                else if (!char.IsWhiteSpace(sb[i - 1]))
                {

                    this.Eraser--;
                }
            }
            return sb.ToString();
        }

        private void ReduceTipDurability(char letter)
        {
            if (char.IsUpper(letter))
            {

                this.Tip -= 2;

            }
            else if (letter != ' ') //should cover all lower case and symbols.
            {

                this.Tip -= 1;
            }
        }
    }
}
