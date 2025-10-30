using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.FrederikBlem
{
    internal class Menu
    {
        internal string DisplayMainMenuAndGetChoice()
        {
            string? mainMenuChoice = "";
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(@$"Choose an options from the following list:
1 - Perform Calculation
2 - History Menu
3 - Quit the program");
            Console.WriteLine("---------------------------------------------");

            mainMenuChoice = Console.ReadLine();

            return mainMenuChoice;
        }

        internal string DisplayHistoryMenuAndGetChoice()
        {
            string? historyMenuChoice = "";
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(@$"History Menu - Choose an option from the following list:");
            Console.WriteLine("1 - Display Operation History");
            Console.WriteLine("2 - Clear Operation History");
            Console.WriteLine("3 - Use a Result from Operation History");
            Console.WriteLine("4 - Return to Main Menu");
            Console.WriteLine("---------------------------------------------");
            historyMenuChoice = Console.ReadLine();

            return historyMenuChoice;
        }

        internal string DisplayOperatorMenuAndGetChoice()
        {
            string? operatorMenuChoice = "";
            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            operatorMenuChoice = Console.ReadLine();

            return operatorMenuChoice;
        }
    }
}
