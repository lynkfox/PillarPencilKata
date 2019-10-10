using System;
using PencilLib;

namespace PencilSimulation
{
    class Program
    {
        
        
        static void Main(string[] args)
        {
            int optionSelect;

            var paper = new Paper();
            var pencil = new Pencil();

            Console.WriteLine("*****     Pencil Simulation 1.0     *****");
            Console.Write("\n\r\n\r");
            Console.WriteLine("Select from the following list of options: ");
            Console.WriteLine("1. New Paper\r\n2. Quit");

            bool validOption = int.TryParse(Console.ReadLine(), out optionSelect);


            while (!validOption || optionSelect < 1 || optionSelect > 2)
            {
                WriteInvalidInput();
                Console.WriteLine("1. New Paper\r\n2. Quit");
                validOption = int.TryParse(Console.ReadLine(), out optionSelect);
            }

            switch (optionSelect)
            {
                case 1:
                    NewPaper();
                    break;
                case 2:
                    Quit();
                    break;
            }

            PencilActionMenu(out optionSelect, out validOption);
            while (!validOption || optionSelect < 1 || optionSelect > 8)
            {
                WriteInvalidInput();
                PencilActionMenu(out optionSelect, out validOption);
            }

            while (optionSelect != 8)
            {
                switch (optionSelect)
                {
                    case 1: //Write
                        Write();
                        break;
                    case 2: //Erase
                        Erase();
                        break;
                    case 3: //Edit
                        break;
                    case 4: //Read
                        Read();
                        break;
                    case 5: //Read
                        NewPaper();
                        break;
                    case 6: //Read
                        Sharpen();
                        break;
                    case 7: //Read
                        NewPencil();
                        break;
                    case 8: //Quit
                        Quit();
                        break;
                }

                PencilActionMenu(out optionSelect, out validOption);
                while (!validOption || optionSelect < 1 || optionSelect > 8)
                {
                    WriteInvalidInput();
                    PencilActionMenu(out optionSelect, out validOption);
                }
            }







            /* Local Functions 
             */

            /* Quit
             */
            void Quit()
            {
                Console.WriteLine("Thank you for using Pencil Simulator 1.0. Goodbye!");
                Environment.Exit(0);
            }

            /*gives up a  new sheet of paper
             */

            void NewPaper()
            {
                paper.NewSheet();
                Console.WriteLine("... ... ... New piece of Paper ready!");
                NewPencil();
            }



            /* Gives a menu to select a small, big, or custom sized pencil
             */
            void NewPencil()
            {
                Console.Write("\r\n");
                int newPencilOption;
                Console.WriteLine("What Kind of Pencil would you like? ");
                Console.Write("\n\r\n\r");
                Console.WriteLine("Select from the following list of options: ");
                Console.WriteLine("1. Small Pencil\r\n2. Big Pencil\r\n3. Custom Pencil\r\n4.Quit\r\n");




                while (!(int.TryParse(Console.ReadLine(), out newPencilOption)) || newPencilOption < 1 || newPencilOption > 4)
                {
                    Console.WriteLine("Invalid Selection. Please try again.");
                    Console.WriteLine("1. Small Pencil\r\n2. Big Pencil\r\n3. Custom Pencil\r\n4.Quit");
                }

                switch (newPencilOption)
                {
                    case 1:
                        SmallPencil();
                        break;
                    case 2:
                        BigPencil();
                        break;
                    case 3:
                        CustomPencil();
                        break;
                    case 4:
                        Quit();
                        break;
                }


            }



            void SmallPencil()
            {
                pencil = new Pencil(40, 5, 10);
                Console.WriteLine("Small pencil can write between 20 and 40 characters, be sharpened 5 times, and erase 10 characters. Go write!\r\n\r\n");

            }

            void BigPencil()
            {
                pencil = new Pencil(80, 10, 20);
                Console.WriteLine("A Big pencil can write between 40 and 80 characters, be sharpened 10 times, and erase 20 characters. Go write!\r\n\r\n");

            }

            void CustomPencil()
            {
                int length, tip, eraser;

                string input;

                Console.WriteLine("Enter Length: ");
                input = Console.ReadLine();

                length = ErrorHandle(input);

                Console.WriteLine("Enter Tip Durability: ");
                input = Console.ReadLine();

                tip = ErrorHandle(input);

                Console.WriteLine("Enter Eraser Durability: ");
                input = Console.ReadLine();

                eraser = ErrorHandle(input);


                Console.WriteLine("Allright. Pencil created with " + tip + " tip durability, " + eraser + " eraser durability, and a length of " + length +"\r\n\r\n");

                pencil = new Pencil(tip, length, eraser);


            }

            /*Verifies for valid inputs for Pencil Constructor
             * 
             * Even though there are Exception Throws in Pencil, those are for realizing there is a problem
             * while coding - this wraper should not force the user to deal with those exceptions for Pencil Constructors
             * instead making sure they are not thrown in the first place
             */
            int ErrorHandle(string value)
            {
                bool NaN = int.TryParse(value, out int number);
                while (!NaN || number <= 0)
                {
                    Console.Write("\r\n");
                    Console.WriteLine("Sorry! That is not a valid input. Please enter a number above 0!");
                    value = Console.ReadLine();

                    NaN = int.TryParse(value, out number);


                }

                return number;
            }

            void WriteInvalidInput()
            {
                Console.Write("\r\n");
                Console.WriteLine("Invalid Selection. Please try again.");
            }

            void Sharpen()
            {
                Console.Write("\r\n");
                pencil.Sharpen();
                Console.Write("... ... ... Pencil Sharpened! Tip durability is once again " + pencil.Tip+ ". You have "+pencil.Length+" length of pencil left. \r\n");
                Console.Write("\r\n");
            }

            void Write()
            {
                Console.Write("\r\n");
                Console.WriteLine("What do you want to write on your paper?");
                string input = Console.ReadLine();
                paper.Prose(pencil.Write(input));
                Console.Write("\r\n");
            }

            void Read()
            {
                Console.Write("\r\n --- Your Paper -- \r\n");
                Console.WriteLine(paper.Content);
                Console.Write("\r\n");
            }

            void Erase()
            {
                Console.Write("\r\n");
                Console.WriteLine("What would you like to erase?");
                string input = Console.ReadLine();
                paper.Delete(pencil.Erase(input));
                Console.Write("\r\n");
            }
        }

        private static void PencilActionMenu(out int optionSelect, out bool validOption)
        {
            Console.WriteLine("Now what would you like do now?\r\n1. Write\r\n2. Erase\r\n3. Edit\r\n4. Read\r\n5. New Paper (Will trash current page!)\r\n6. Sharpen Pencil\r\n7. New Pencil\r\n8. Quit\r\n");
            validOption = validOption = int.TryParse(Console.ReadLine(), out optionSelect);
        }

    }
}
