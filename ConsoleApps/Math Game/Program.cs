//task - https://www.thecsharpacademy.com/project/53/math-game
static class MathGame
{
    static List<MathGameRecord> history = new List<MathGameRecord>();

    static public void Add(int a, int b)
    {
        history.Add(new MathGameRecord(MathGameRecord.Operation.Add, a, b));
        Console.WriteLine(a + b);
    }
    static public void Subtract(int a, int b)
    {
        history.Add(new MathGameRecord(MathGameRecord.Operation.Subtract, a, b));
        Console.WriteLine(a - b);
    }

    static public void Multiply(int a, int b)
    {
        history.Add(new MathGameRecord(MathGameRecord.Operation.Multiply, a, b));
        Console.WriteLine(a * b);
    }

    static public void Divide(int a, int b)
    {
        if (b == 0)
        {
            Console.WriteLine("ERROR: Attempted to divide by zero");
        }

        if (a % b != 0)
        {
            Console.WriteLine("ERROR: Division can't be completed without remainder");
        }

        history.Add(new MathGameRecord(MathGameRecord.Operation.Divide, a, b));
        Console.WriteLine(a / b);
    }
    
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Press 1 to Add");
            Console.WriteLine("Press 2 to subtract");
            Console.WriteLine("Press 3 to multiply");
            Console.WriteLine("Press 4 to divide");
            Console.WriteLine("Press 5 to show history");
            Console.WriteLine("Press esc to end the program");

            ConsoleKeyInfo key = Console.ReadKey();
            Console.Clear();
            if (key.KeyChar == '\u001b')
            {
                Console.WriteLine("Ending the program");
                break;
            }

            if (key.KeyChar == '5')
            {
                Console.WriteLine("Game History: ");
                foreach(MathGameRecord entry in history)
                {
                    Console.WriteLine($"Operation: {entry.operation}, {entry.firstNumber}, {entry.secondNumber}");
                }
                Console.WriteLine("");
                continue;
            }
            int firstNumber;
            int secondNumber;

            Console.WriteLine("Enter First Number: ");
            while (!Int32.TryParse(Console.ReadLine(), out firstNumber))
                Console.WriteLine("Entered invalid number");


            Console.Clear();
            Console.WriteLine("Enter Second Number: ");
            while (!Int32.TryParse(Console.ReadLine(), out secondNumber))
                Console.WriteLine("Entered invalid number");
            Console.Clear();
            Console.WriteLine("Your result is: ");
            if (key.KeyChar == '1')
                Add(firstNumber, secondNumber);
            if (key.KeyChar == '2')
                Subtract(firstNumber, secondNumber);
            if (key.KeyChar == '3')
                Multiply(firstNumber, secondNumber);
            if (key.KeyChar == '4')
                Divide(firstNumber, secondNumber);
            Console.WriteLine("");
        }
    }

}

class MathGameRecord
{
    public Operation operation;

    public int firstNumber;

    public int secondNumber;

    public MathGameRecord(Operation op, int a, int b)
    {
        operation = op;
        firstNumber = a;
        secondNumber = b;
    }  

    public enum Operation
    {
        Add,  // 0
        Subtract,   // 1
        Multiply,  // 2
        Divide  //3
    };
}