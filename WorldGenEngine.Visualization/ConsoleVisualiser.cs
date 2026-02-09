namespace WorldGenEngine.Visualization;

abstract class ConsoleVisualiser
{
    protected virtual int GetLineLength()
    {
        return 1;
    }

    protected void DrawLine(char symbol)
    {
        Console.WriteLine(new string(symbol, GetLineLength()));
    }
}