using System;
using PencilLib;

namespace PencilSimulation
{
    class Program
    {
        
        
        static void Main(string[] args)
        {
            int optionSelect;
            bool validOption;

            var paper = new Paper();
            var pencil = new Pencil();



            Console.WriteLine("*****     Pencil Simulation 1.0     *****");
            NewLine();
            NewLine();
            Console.WriteLine("Select from the following list of options: ");
            Console.WriteLine("1. New Paper\r\n2. Quit");

            InitialMenuOptions(out optionSelect, out validOption);

            while (!validOption || optionSelect < 1 || optionSelect > 2)
            {
                WriteInvalidInput();
                Console.WriteLine("1. New Paper\r\n2. Quit");
                InitialMenuOptions(out optionSelect, out validOption);
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
                        Edit();
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

            void Quit()
            {
                NewLine();
                Console.WriteLine("Thank you for using Pencil Simulator 1.0. Goodbye!");
                Environment.Exit(0);
            }

            void NewPaper()
            {
                NewLine();
                paper.NewSheet();
                Console.WriteLine("... ... ... New piece of Paper ready!");
                NewPencil();
            }
         
            void NewPencil()
            {
                NewLine();
                int newPencilOption;
                Console.WriteLine("What Kind of Pencil would you like? ");
                NewLine();
                Console.WriteLine("Select from the following list of options: ");
                Console.WriteLine("1. Small Pencil\r\n2. Big Pencil\r\n3. Custom Pencil\r\n4.Quit\r\n");




                while (!(int.TryParse(Console.ReadLine(), out newPencilOption)) || newPencilOption < 1 || newPencilOption > 4)
                {
                    Console.WriteLine("Invalid Selection. Please try again.");
                    Console.WriteLine("1. Small Pencil\r\n2. Big Pencil\r\n3. Custom Pencil\r\n4. Quit");
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

            void NewLine()
            {
                Console.Write("\r\n");
            }

            void Sharpen()
            {
                NewLine();
                pencil.Sharpen();
                Console.Write("... ... ... Pencil Sharpened! Tip durability is once again " + pencil.Tip+ ". You have "+pencil.Length+" length of pencil left. \r\n");
                NewLine();
            }

            void Write()
            {
                NewLine();
                Console.WriteLine("What do you want to write on your paper?");
                string input = Console.ReadLine();
                paper.Prose(pencil.Write(input));
                NewLine();
                Read();
            }

            void Read()
            {
                Console.Write("\r\n --- Your Paper -- \r\n");
                Console.WriteLine(paper.Content);
                NewLine();
            }

            void Erase()
            {
                NewLine();
                Console.WriteLine("What would you like to erase?");
                string input = Console.ReadLine();
                try
                {
                    paper.Delete(pencil.Erase(input));
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                NewLine();
                Read();
            }

            void Edit()
            {
                NewLine();
                Console.WriteLine("What would you like to edit into the last empty space created?");
                string input = Console.ReadLine();
                paper.Edit(pencil.Write(input));
                NewLine();
                Read();


            }
        }

        private static void InitialMenuOptions(out int optionSelect, out bool validOption)
        {
            validOption = int.TryParse(Console.ReadLine(), out optionSelect);
        }

        private static void PencilActionMenu(out int optionSelect, out bool validOption)
        {
            Console.WriteLine("Now what would you like do now?\r\n1. Write\r\n2. Erase\r\n3. Edit\r\n4. Read\r\n5. New Paper (Will trash current page!)\r\n6. Sharpen Pencil\r\n7. New Pencil\r\n8. Quit\r\n");
            validOption = validOption = int.TryParse(Console.ReadLine(), out optionSelect);
        }

    }
}
