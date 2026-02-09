using System.Runtime.CompilerServices;

namespace WorldGenEngine.Core.MatrixGeneration.Models;

/// <summary>
/// Model implementing a binary matrix
/// </summary>
/// <param name="width"></param>
/// <param name="height"></param>
/// <param name="matrix"></param>
public readonly struct BinaryMatrix(int width, int height, bool[] matrix) : IMatrix
{
    public int Width { get; } = width;
    public int Height { get; } = height;

    public bool GetValue(int x, int y)
    {
        return Matrix[Width * y + x];
    }

    public bool[] GetFlatArrayClone()
    {
        return (bool[])Matrix.Clone();
    }

    public void SetValue(int x, int y, bool value)
    {
        Matrix[Width * y + x] = value;
    }

    public bool[] Matrix { get; } = matrix;

    public int Length => Matrix.Length;
    public bool this[int index] => Matrix[index];
}