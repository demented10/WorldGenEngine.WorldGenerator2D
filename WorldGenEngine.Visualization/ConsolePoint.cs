namespace WorldGenEngine.Visualization;

struct ConsolePoint
{
    public static int W = 2;

    public char Value { get; } = 'T';
    public ConsoleColor BackgroundColor { get; }

    public ConsolePoint(char value, ConsoleColor backgroundColor = ConsoleColor.Black)
    {
        Value = value;
        BackgroundColor = backgroundColor;
    }
}