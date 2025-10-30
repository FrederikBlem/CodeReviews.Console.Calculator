// CalculatorLibrary.cs
namespace Models
{
    internal class OperationRecord
    {
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public OperationType Operation { get; set; }
        public double Result { get; set; }
    }

    internal enum OperationType
    {
        Add = 0,
        Subtract = 1,
        Multiply = 2,
        Divide = 3
    }
}