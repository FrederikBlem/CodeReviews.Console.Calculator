namespace Calculator.FrederikBlem;
internal class Menu
{
    private string[] validOptions = new string[] { "a", "s", "m", "d", "r", "p" };
    private string[] validTrigOptions = new string[] { "cos", "acos", "sin", "asin", "tan", "atan" };
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
        Console.WriteLine("4 - Save History");
        Console.WriteLine("5 - Return to Main Menu");
        Console.WriteLine("---------------------------------------------");
        historyMenuChoice = Console.ReadLine();

        return historyMenuChoice.Trim().ToLower();
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
        Console.WriteLine("------------------------");
        Console.WriteLine("\tr - Nth Root (Enter 2 next for square root or 3 for cube root etc. Your next number will get rounded.)");
        Console.WriteLine("\tp - Power");
        Console.WriteLine("------------------------");
        Console.WriteLine("\tcos - cosinus");
        Console.WriteLine("\tacos - arcus cosinus");
        Console.WriteLine("\tsin - sinus");
        Console.WriteLine("\tasin - arcus sinus");
        Console.WriteLine("\ttan - tangens");
        Console.WriteLine("\tatan - arcus tangens");

        Console.Write("Your option? ");

        operatorMenuChoice = Console.ReadLine();

        return operatorMenuChoice.Trim().ToLower();
    }

    internal double PromptGetValidNumber(bool isFirstNumber = true)
    {
        string? numInput;
        if (isFirstNumber)
        {
            Console.Write("Type a number (or a number representing degrees), and then press Enter: ");
        }
        else
        {
            Console.Write("Type another number, and then press Enter: ");
        }
        
        numInput = Console.ReadLine();

        double cleanNum;
        while (!double.TryParse(numInput, out cleanNum))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput = Console.ReadLine();
        }

        return cleanNum;
    }

    internal int PromptGetValidEntryNumber()
    {
        Console.Write("Enter the entry number to use its result: ");
        string? entryInput = Console.ReadLine();
        int entryNumber = 0;
        while (!int.TryParse(entryInput, out entryNumber) || entryNumber < 1)
        {
            Console.Write("This is not valid input. Please enter a valid entry number: ");
            entryInput = Console.ReadLine();
        }
        return entryNumber;
    }

    // This probably belongs in another class. Please advice.
    internal int ValidateInputAndPerformOperation(string operatorInput, CalculatorLibrary.Calculator calculator, double cleanNum1, Menu menu )
    {
        bool isValidOption = false;
        
        foreach (string option in validOptions)
        {
            if (option.Equals(operatorInput)) 
            { 
                isValidOption = true;
            }
        }

        bool isValidTrigOption = false;
        if (!isValidOption) 
        { 
            foreach (string trigOption in validTrigOptions)
            {
                if( trigOption.Equals(operatorInput))
                {
                    isValidTrigOption = true;
                }
            }
        }

        if (operatorInput == null || !(isValidOption || isValidTrigOption))
        {
            Console.WriteLine("Error: Unrecognized input.");
            return 0;
        }
       
        if (isValidOption)
        {
            try
            {
                double cleanNum2 = menu.PromptGetValidNumber(isFirstNumber: false);

                double result = calculator.DoOperation(cleanNum1, cleanNum2, operatorInput);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                    return 0;
                }
                else
                {
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                    return 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                return 0;
            }
        }

        if (isValidTrigOption)
        {
            double result = calculator.DoTrigonometricOperation(cleanNum1, operatorInput);
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
                return 0;
            }
            else
            {
                Console.WriteLine("Your result: {0:0.##}\n", result);
                return 1;
            }
        }

        return 0;
    }
}
