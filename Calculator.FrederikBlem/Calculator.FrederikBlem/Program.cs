using System.Text.RegularExpressions;
using System.Globalization;
using System.Runtime.CompilerServices;
using Calculator.FrederikBlem;

internal class Program
{
    private static void Main(string[] args)
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-US"); // I want to use dot as decimal separator

        int timesCalculated = 0;
        bool endApp = false;
        Menu menu = new();
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
                            Console.Clear();
                            
                            double cleanNum1 = menu.PromptGetValidNumber();
                            string operatorInput = menu.DisplayOperatorMenuAndGetChoice();

                            //TODO: refactor so I don't repeat too much code (Similar 1) and fix Regex statement order
                            if (operatorInput == null || !Regex.IsMatch(operatorInput, "[a|s|m|d|r|p]"))
                            {
                                Console.WriteLine("Error: Unrecognized input.");
                            }
                            else if (Regex.IsMatch(operatorInput, "[acos|cos|asin|sin|atan|tan]"))
                            {
                                double result = calculator.DoTrigonometricOperation(cleanNum1, operatorInput);
                                if (double.IsNaN(result))
                                {
                                    Console.WriteLine("This operation will result in a mathematical error.\n");
                                }
                                else
                                {
                                    Console.WriteLine("Your result: {0:0.##}\n", result);
                                    timesCalculated++;
                                }
                            }
                            else
                            {
                                try
                                {
                                    double cleanNum2 = menu.PromptGetValidNumber(isFirstNumber: false);

                                    double result = calculator.DoOperation(cleanNum1, cleanNum2, operatorInput);
                                    if (double.IsNaN(result))
                                    {
                                        Console.WriteLine("This operation will result in a mathematical error.\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Your result: {0:0.##}\n", result);
                                        timesCalculated++;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                                }
                            }
                            Console.WriteLine("------------------------\n");

                            string? continueInput = "";
                            Console.Write("Press 'c' to continue calculating, or any other key to return to the main menu: ");
                            continueInput = Console.ReadLine();
                            if (continueInput != "c")
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

                                    int entryNumber = menu.PromptGetValidEntryNumber();
                                    
                                    double entryResult = CalculatorLibrary.Calculator.GetOperationRecordResult(entryNumber);
                                    Console.Clear();
                                    Console.WriteLine($"The result from entry {entryNumber} is: {entryResult}\n");

                                    string operatorInput = menu.DisplayOperatorMenuAndGetChoice();

                                    //TODO: refactor so I don't repeat too much code (Similar 2) and fix Regex statement order
                                    if (operatorInput == null || !Regex.IsMatch(operatorInput, "[a|s|m|d|r|p]"))
                                    {
                                        Console.WriteLine("Error: Unrecognized input.");
                                    }
                                    else if (Regex.IsMatch(operatorInput, "[acos|cos|asin|sin|atan|tan]"))
                                    {
                                        double result = calculator.DoTrigonometricOperation(entryResult, operatorInput);
                                        if (double.IsNaN(result))
                                        {
                                            Console.WriteLine("This operation will result in a mathematical error.\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Your result: {0:0.##}\n", result);
                                            timesCalculated++;
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            double cleanNum2 = menu.PromptGetValidNumber(isFirstNumber: false);

                                            double result = calculator.DoOperation(entryNumber, cleanNum2, operatorInput);
                                            if (double.IsNaN(result))
                                            {
                                                Console.WriteLine("This operation will result in a mathematical error.\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Your result: {0:0.##}\n", result);
                                                timesCalculated++;
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                                        }
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
                        Console.Clear();
                        Console.WriteLine($"The application has been used to calculate {timesCalculated} times this session.");
                        Console.WriteLine("------------------------");
                        Console.WriteLine("Save history to file before exit? y/n");

                        string? saveInput = Console.ReadLine();
                        while (saveInput != "y" && saveInput != "n")
                        {
                            Console.WriteLine("Invalid input. Please enter 'y' or 'n': ");
                            saveInput = Console.ReadLine();
                        }
                        if (saveInput.Trim().ToLower() == "y")
                        {
                            Console.WriteLine("Saving history to file...");
                            calculator.SaveHistoryToJSONFile();
                            Console.WriteLine("History saved.");
                        }
                        if (saveInput.Trim().ToLower() == "n")
                        {
                            Console.WriteLine("History not saved.");
                        }
                        Console.WriteLine("------------------------\n");
                        Console.WriteLine("Press Enter to exit the application.");
                        Console.ReadLine();
                        exitMainMenu = true;
                        endApp = true;
                        break;
                    default:
                        break; 
                }                            
            } while (!exitMainMenu);
        }
    }
}