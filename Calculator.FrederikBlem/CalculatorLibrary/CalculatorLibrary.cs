// CalculatorLibrary.cs
using System.Text.Json;
using Models;
namespace CalculatorLibrary;
public class Calculator
{
    static List<OperationRecord> operationRecords = new List<OperationRecord>();
    StreamWriter logFile;
    public Calculator()
    {
        string filePath = $"{Directory.GetCurrentDirectory()}\\calculatorlog.json";
        if (File.Exists(filePath))
        {
            LoadHistoryFromJSONFile();
        }
    }
    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        OperationType opType = OperationType.Add;
        switch (op)
        {
            case "a":
                result = num1 + num2;
                opType = OperationType.Add;
                break;
            case "s":
                result = num1 - num2;
                opType = OperationType.Subtract;
                break;
            case "m":
                result = num1 * num2;
                opType = OperationType.Multiply;
                break;
            case "d":
                while(num2 == 0)
                {
                    Console.WriteLine("Division by zero is not allowed. Please type a non-zero number.");
                    if(double.TryParse(Console.ReadLine(), out double newNum2))
                    {
                        num2 = newNum2;
                    }
                }
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                opType = OperationType.Divide;
                break;
            case "r":
                result = NthRoot(num1, (int)num2);
                opType = OperationType.Root; // Using Multiply as a placeholder since Sqrt is not defined in OperationType
                break;
            case "p":
                result = Math.Pow(num1, num2);
                opType = OperationType.Power; // Using Multiply as a placeholder since Pow is not defined in OperationType
                break;

            default:
                break;
        }
        AddOperationRecord(num1, num2, opType, result);

        return result;
    }

    static double NthRoot(double A, int N)
    {
        return Math.Pow(A, 1.0 / N);
    }

    #region Operation Record (List) Handling
    private static void AddOperationRecord(double operand1, double operand2, OperationType opType, double result)
    {
        OperationRecord record = new OperationRecord
        {
            Operand1 = operand1,
            Operand2 = operand2,
            Operation = opType,
            Result = result
        };
        operationRecords.Add(record);
    }

    public void DisplayOperationRecords()
    {
        int i = 0;
        string operationSymbol = "";
        foreach (var record in operationRecords)
        {
            i++;
            
            switch (record.Operation)
            {
                case OperationType.Add:
                    operationSymbol = "+";
                    break;
                case OperationType.Subtract:
                    operationSymbol = "-";
                    break;
                case OperationType.Multiply:
                    operationSymbol = "*";
                    break;
                case OperationType.Divide:
                    operationSymbol = "/";
                    break;
                case OperationType.Root:
                    operationSymbol = "√";
                    break;
                case OperationType.Power:
                    operationSymbol = "^";
                    break;
                default:
                    break;
            }

            Console.WriteLine($"Operation {i}: {record.Operand1} {operationSymbol} {record.Operand2} = {record.Result}");
        }
    }

    public static int GetOperationRecordResult(int entryNumber)
    {
        while(entryNumber < 1 || entryNumber > operationRecords.Count)
        {
            Console.Write("This is not valid input. Please enter a valid entry number: ");
            string? entryInput = Console.ReadLine();
            if(int.TryParse(entryInput, out int newEntryNumber))
            {
                entryNumber = newEntryNumber;
            }
        }
        return (int)operationRecords[entryNumber - 1].Result;
    }
    #endregion // Operation Record (List) Handling

    #region File and Json Handling
    public void SaveHistoryToJSONFile() // Load has already been implemented in the constructor, so we don't need to append to the file here.
    {
        List<OperationRecord> _data = new List<OperationRecord>();

        foreach (var record in operationRecords)
        {
            _data.Add(new OperationRecord
            {
                Operand1 = record.Operand1,
                Operand2 = record.Operand2,
                Operation = record.Operation,
                Result = record.Result
            });
        }

        string json = JsonSerializer.Serialize(_data);
        File.WriteAllText($"{Directory.GetCurrentDirectory()}\\calculatorlog.json", json);
    }

    public void LoadHistoryFromJSONFile()
    {
        string filePath = $"{Directory.GetCurrentDirectory()}\\calculatorlog.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            List<OperationRecord>? loadedRecords = JsonSerializer.Deserialize<List<OperationRecord>>(json);
            if (loadedRecords != null)
            {
                operationRecords = loadedRecords;
            }
        }
    }

    public void ClearOperationRecords()
    {
        operationRecords.Clear();
        string filePath = $"{Directory.GetCurrentDirectory()}\\calculatorlog.json";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
    #endregion // File and Json Handling
}