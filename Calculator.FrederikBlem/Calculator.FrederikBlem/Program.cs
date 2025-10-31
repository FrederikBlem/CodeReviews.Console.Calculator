using System.Globalization;
using Calculator.FrederikBlem;

internal class Program
{
    private static void Main(string[] args)
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-US"); // I want to use dot as decimal separator

        int timesCalculated = 0;
        bool endApp = false;

        string lineSpacing = "------------------------------------------------------------------------------------------";

        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine(lineSpacing);

        CalculatorLibrary.Calculator calculator = new();

        while (!endApp)
        {
            bool exitMainMenu = false;
            do {
                
                var mainMenuChoice = Menu.DisplayMainMenuAndGetChoice();

                switch (mainMenuChoice)
                {
                    case "1": 
                        exitMainMenu = false;
                        bool inCalculationMenu = true;
                        while (inCalculationMenu)
                        {
                            Console.Clear();
                            
                            double cleanNum1 = Helper.PromptGetValidNumber();
                            string operatorInput = Menu.DisplayOperatorMenuAndGetChoice();

                            timesCalculated += Helper.ValidateInputAndPerformOperation(operatorInput, calculator, cleanNum1);

                            Console.WriteLine(lineSpacing);

                            string? continueInput;
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
                            var historyMenuChoice = Menu.DisplayHistoryMenuAndGetChoice();

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

                                    int entryNumber = Helper.PromptGetValidEntryNumber();
                                    
                                    double entryResult = calculator.GetOperationRecordResult(entryNumber);
                                    Console.Clear();
                                    Console.WriteLine($"The result from entry {entryNumber} is: {entryResult}\n");

                                    string operatorInput = Menu.DisplayOperatorMenuAndGetChoice();

                                    timesCalculated += Helper.ValidateInputAndPerformOperation(operatorInput, calculator, entryResult);

                                    Console.WriteLine(lineSpacing);
                                    break;
                                case "4":
                                    Console.Clear();
                                    Console.WriteLine("Saving history to file...");
                                    calculator.SaveHistoryToJSONFile();
                                    Console.WriteLine("History saved.");
                                    break;
                                case "5":
                                    Console.Clear();
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
                        Console.WriteLine(lineSpacing);
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
                        Console.WriteLine(lineSpacing);
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