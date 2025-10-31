using Calculator.FrederikBlem;

namespace Calculator.FrederikBlem;

internal class Helper
{
    static readonly string[] validOptions = ["a", "s", "m", "d", "r", "p"];
    static readonly string[] validTrigOptions = ["cos", "acos", "sin", "asin", "tan", "atan"];
    internal static double PromptGetValidNumber(bool isFirstNumber = true)
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

    internal static int PromptGetValidEntryNumber()
    {
        Console.Write("Enter the entry number to use its result: ");
        string? entryInput = Console.ReadLine();
        int entryNumber;
        while (!int.TryParse(entryInput, out entryNumber) || entryNumber < 1)
        {
            Console.Write("This is not valid input. Please enter a valid entry number: ");
            entryInput = Console.ReadLine();
        }
        return entryNumber;
    }

    internal static int ValidateInputAndPerformOperation(string operatorInput, CalculatorLibrary.Calculator calculator, double cleanNum1)
    {
        bool isValidOption = GetIsValidOption(validOptions, operatorInput);

        bool isValidTrigOption = GetIsValidOption(validTrigOptions, operatorInput);

        if (operatorInput == null || !(isValidOption || isValidTrigOption))
        {
            Console.WriteLine("Error: Unrecognized input.");
            return 0;
        }

        if (isValidOption || isValidTrigOption)
        {
            string mathErrorMessage = "This operation will result in a mathematical error.\n";
            string resultMessage = "Your result: ";
            try
            {
                if (isValidOption) 
                {
                    double cleanNum2 = Helper.PromptGetValidNumber(isFirstNumber: false);

                    double result = calculator.DoOperation(cleanNum1, cleanNum2, operatorInput);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine(mathErrorMessage);
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine(resultMessage + result);
                        return 1;
                    }
                }
                else
                {
                    double result = calculator.DoTrigonometricOperation(cleanNum1, operatorInput);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine(mathErrorMessage);
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine(resultMessage + result);
                        return 1;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                return 0;
            }
        }

        return 0;
    }

    internal static bool GetIsValidOption(string[] options, string operatorInput)
    {
        foreach (string option in options)
        {
            if (option.Equals(operatorInput))
            {
                return true;
            }
        }
        return false;
    }
}
