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
        Divide = 3,
        Root = 4,
        Power = 5,
        Cosinus = 6,
        ArcusCosinus = 7,
        Sinus = 8,
        ArcusSinus = 9,
        Tangens = 10,
        ArcusTangens = 11
    }
}