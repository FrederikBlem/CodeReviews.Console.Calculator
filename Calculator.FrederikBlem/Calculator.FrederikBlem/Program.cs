using System.Text.RegularExpressions;
using System.Globalization;
using System.Runtime.CompilerServices;
using Calculator.FrederikBlem;

internal class Program
{
    private static void Main(string[] args)
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-US"); // I want to use dot as decimal separator

        bool endApp = false;
        Menu menu = new();
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        CalculatorLibrary.Calculator calculator = new();

        while (!endApp)
        {
            bool exitMainMenu = false;
            do {
                
                var mainMenuChoice = menu.DisplayMainMenuAndGetChoice();

                switch (mainMenuChoice)
                {
                    case "1":
                        exitMainMenu = false;
                        bool inCalculationMenu = true;
                        while (inCalculationMenu)
                        {
                            // Declare variables and set to empty.
                            // Use Nullable types (with ?) to match type of System.Console.ReadLine
                            string? numInput1 = "";
                            string? numInput2 = "";
                            double result = 0;

                            // Ask the user to type the first number.
                            Console.Write("Type a number, and then press Enter: ");
                            numInput1 = Console.ReadLine();

                            double cleanNum1 = 0;
                            while (!double.TryParse(numInput1, out cleanNum1))
                            {
                                Console.Write("This is not valid input. Please enter a numeric value: ");
                                numInput1 = Console.ReadLine();
                            }

                            string operatorInput = menu.DisplayOperatorMenuAndGetChoice();

                            // Ask the user to type the second number.
                            Console.Write("Type another number, and then press Enter: ");
                            numInput2 = Console.ReadLine();

                            double cleanNum2 = 0;
                            while (!double.TryParse(numInput2, out cleanNum2))
                            {
                                Console.Write("This is not valid input. Please enter a numeric value: ");
                                numInput2 = Console.ReadLine();
                            }

                            // Validate input is not null, and matches the pattern
                            if (operatorInput == null || !Regex.IsMatch(operatorInput, "[a|s|m|d]"))
                            {
                                Console.WriteLine("Error: Unrecognized input.");
                            }
                            else
                            {
                                try
                                {
                                    result = calculator.DoOperation(cleanNum1, cleanNum2, operatorInput);
                                    if (double.IsNaN(result))
                                    {
                                        Console.WriteLine("This operation will result in a mathematical error.\n");
                                    }
                                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                                }
                            }
                            Console.WriteLine("------------------------\n");
                            inCalculationMenu = false;
                        }
                        break;
                    case "2":
                        exitMainMenu = false;
                        bool inHistoryMenu = true;
                        while (inHistoryMenu)
                        {                            
                            var historyMenuChoice = menu.DisplayHistoryMenuAndGetChoice();

                            switch (historyMenuChoice)
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Operation History:");
                                    calculator.DisplayOperationRecords();
                                    break;
                                case "2":
                                    Console.Clear();
                                    Console.WriteLine("Clearing Operation History:");
                                    calculator.ClearOperationRecords();
                                    break;
                                case "3":
                                    Console.Clear();
                                    Console.WriteLine("Use a Result from Operation History:");
                                    calculator.DisplayOperationRecords();
                                    Console.Write("Enter the entry number to use its result: ");
                                    string? entryInput = Console.ReadLine();
                                    int entryNumber = 0;
                                    while (!int.TryParse(entryInput, out entryNumber) || entryNumber < 1)
                                    {
                                        Console.Write("This is not valid input. Please enter a valid entry number: ");
                                        entryInput = Console.ReadLine();
                                    }
                                    double entryResult = CalculatorLibrary.Calculator.GetOperationRecordResult(entryNumber);
                                    Console.Clear();
                                    Console.WriteLine($"The result from entry {entryNumber} is: {entryResult}\n");

                                    string operatorInput = menu.DisplayOperatorMenuAndGetChoice();
                                    Console.Write("Type another number, and then press Enter: ");

                                    string? numInput2 = Console.ReadLine();
                                    double cleanNum2;
                                    while (!double.TryParse(numInput2, out cleanNum2))
                                    {
                                        Console.Write("This is not valid input. Please enter a numeric value: ");
                                        numInput2 = Console.ReadLine();
                                    }

                                    double result;
                                    try
                                    {
                                        result = calculator.DoOperation(entryResult, cleanNum2, operatorInput);
                                        if (double.IsNaN(result))
                                        {
                                            Console.WriteLine("This operation will result in a mathematical error.\n");
                                        }
                                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                                    }
                                    Console.WriteLine("------------------------\n");
                                    break;
                                case "4":
                                    inHistoryMenu = false;
                                    break;
                                default:
                                    Console.WriteLine("Invalid option. Please choose again.");
                                    break;
                            }
                        }
                        break;
                    case "3":
                        exitMainMenu = true;
                        endApp = true;
                        break;
                    default:
                        break; 
                }                            
            } while (!exitMainMenu);

            

            //// Wait for the user to respond before closing.
            //Console.WriteLine("Choose an option from the following list:");
            //Console.WriteLine("\tn - Exit");
            //Console.WriteLine("\td - Display Operation History");
            //Console.WriteLine("\tc - Clear Operation History");
            //Console.Write("Your option? ");
            //string? response = Console.ReadLine();
            //if (response == "n") 
            //{ 
            //    endApp = true;
            //    calculator.SaveHistoryToJSONFile();
            //}
            //if (response == "d")
            //{
            //    calculator.DisplayOperationRecords();
            //}
            //if (response == "c")
            //{
            //    calculator.ClearOperationRecords();
            //}

            Console.WriteLine("\n"); // Friendly linespacing.
        }
    }
}