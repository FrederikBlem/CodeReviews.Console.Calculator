namespace Calculator.FrederikBlem;
internal class Menu
{
    private static readonly string lineSpacing = "------------------------------------------------------------------------------------------";
    internal static string DisplayMainMenuAndGetChoice()
    {
        string? mainMenuChoice;
        Console.WriteLine(lineSpacing);
        Console.WriteLine(@$"Choose an options from the following list:
1 - Perform Calculation
2 - History Menu
3 - Quit the program");
        Console.WriteLine(lineSpacing);

        mainMenuChoice = Console.ReadLine();

        return mainMenuChoice;
    }

    internal static string DisplayHistoryMenuAndGetChoice()
    {
        string? historyMenuChoice;
        Console.WriteLine(lineSpacing);
        Console.WriteLine(@$"History Menu - Choose an option from the following list:
1 - Display Operation History
2 - Clear Operation History
3 - Use a Result from Operation History
4 - Save History
5 - Return to Main Menu");
        Console.WriteLine(lineSpacing);
        historyMenuChoice = Console.ReadLine();

        return historyMenuChoice.Trim().ToLower();
    }

    internal static string DisplayOperatorMenuAndGetChoice()
    {
        string? operatorMenuChoice;

        Console.WriteLine(@$"Choose an operator from the following list:
	a - Add
	s - Subtract
	m - Multiply
	d - Divide
{lineSpacing}
        r - Nth Root (Enter 2 as next number for square root or 3 for cube root etc.)
	p - Power
{lineSpacing}
        cos - Cosine
	acos - Arc Cosine
	sin - Sine
	asin - Arc Sine
	tan - Tangent
	atan - Arc Tangent");
        Console.Write("Your option? ");

        operatorMenuChoice = Console.ReadLine();

        return operatorMenuChoice.Trim().ToLower();
    }
}
