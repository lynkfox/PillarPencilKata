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



        private int Tip { get; set; }
        private int Length { get; set; }
        private int Eraser { get; set; }


        

        public Pencil() : this(TIP, LENGTH, ERASER)
        {
        }

        public Pencil(int tipStartingValue) : this(tipStartingValue, LENGTH, ERASER)
        {
        }

        public Pencil(int tipDurability, int length) : this(tipDurability, length, ERASER)
        {
        }

        public Pencil(int tipDurability, int length, int eraserDurability)
        {
            SetTipDurability(tipDurability);
            SetLength(length);
            SetEraserDurability(eraserDurability);
        }

        private void SetTipDurability(int tip)
        {
            if (tip <= 0)
            {
                throw new ArgumentOutOfRangeException("PencilTipLessThan1", "Pencil Cannot Have Zero or Negative Tip");
            }
            else
            {
                this.maxDurability = tip;
                this.Tip = this.maxDurability;
            }
        }

        private void SetLength(int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("PencilLengthNegative", "Pencil Cannot Have Negitive Length");
            }
            else
            {
                this.Length = length;
            }
        }

        private void SetEraserDurability(int eraser)
        {
            if (eraser < 0)
            {
                throw new ArgumentOutOfRangeException("PencilEraserNegative", "Pencil Cannot Have Negative Eraser");
            }
            else
            {
                this.Eraser = eraser;
            }
        }



        public int CheckTip()
        {
            return this.Tip;
        }

        public int GetLength()
        {
            return this.Length;
        }

        public int GetEraser()
        {
            return this.Eraser;
        }



        public string Write(string word)
        {

            return  DullTipAndWriteWord(word);

        }


        private string DullTipAndWriteWord(string input)
        {
            string output = "";
            foreach (char letter in input)
            {
                if (this.Tip > 0)
                {
                    output += letter;
                    ReduceTipDurability(letter);
                }
                else
                {
                    output += " ";
                }
            }

            PreventNegativeTip();

            return output;
        }

        private void ReduceTipDurability(char letter)
        {
            if (char.IsUpper(letter))
            {
                this.Tip -= 2;
            }
            else if (!char.IsWhiteSpace(letter))
            {
                this.Tip -= 1;
            }
            //else letter is a WhiteSpace, so do nothing.
        }

        private void PreventNegativeTip()
        {
            if (this.Tip < 0)
            {
                this.Tip = 0;
            }
        }


        public void Sharpen()
        {
            if (this.Length > 0)
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
                //else if it is a whitespace, do nothing
            }
            return sb.ToString();
        }

       
    }
}
