namespace WorldGenEngine.Core.MatrixAlgorithms.Models;

/// <summary>
/// Structure realise rect NxM
/// </summary>
/// <param name="X">Start X axis point</param>
/// <param name="Y">Start Y axis point</param>
/// <param name="Width">size for axis X</param>
/// <param name="Height">size for axis Y</param>

public record struct Rect(int X, int Y, int Width, int Height)
{
    public int X = X;
    public int Y = Y;
    public int Width = Width;
    public int Height = Height;
    public int XMax => X + Width;
    public int YMax => Y + Height;


    public override string ToString()
    {
        return $"X: {X}, Y: {Y}, Width: {Width}, Height: {Height}";
    }
}