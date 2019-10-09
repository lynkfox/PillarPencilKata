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
            Console.WriteLine("1. New Paper\r\n2. New Pencil\r\n3. Quit");

            


            while (!(int.TryParse(Console.ReadLine(), out optionSelect)) || optionSelect<1 || optionSelect>3)
            {
                Console.WriteLine("Invalid Selection. Please try again.");
                Console.WriteLine("1. New Paper\r\n2. New Pencil\r\n3. Quit");
            }

            switch(optionSelect)
            {
                case 1:
                    NewPaper();
                    break;
                case 2:
                    NewPencil();
                    break;
                case 3:
                    Quit();
                    break;
            }



            void Quit()
            {
                Console.WriteLine("Thank you for using Pencil Simulator 1.0. Goodbye!");
                Environment.Exit(0);
            }

            void NewPaper()
            {
                paper.NewSheet();
                Console.WriteLine("... ... ... New piece of Paper ready!");
            }

            void NewPencil()
            {

                int newPencilOption;
                Console.WriteLine("What Kind of Pencil would you like? ");
                Console.Write("\n\r\n\r");
                Console.WriteLine("Select from the following list of options: ");
                Console.WriteLine("1. Small Pencil\r\n2. Big Pencil\r\n3. Custom Pencil\r\n4.Quit");

                


                while (!(int.TryParse(Console.ReadLine(), out newPencilOption)) || newPencilOption < 1 || newPencilOption > 4)
                {
                    Console.WriteLine("Invalid Selection. Please try again.");
                    Console.WriteLine("1. Small Pencil\r\n2. Big Pencil\r\n3. Custom Pencil\r\n4.Quit");
                }

                switch (newPencilOption)
                {
                    case 1:
                        //SmallPencil();
                        break;
                    case 2:
                        //BigPencil();
                        break;
                    case 3:
                        //CustomPencil();
                        break;
                    case 4:
                        Quit();
                        break;
                }


            }
        }

        
    }
}
