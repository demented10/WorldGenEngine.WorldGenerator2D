namespace WorldGenEngine.Core.MatrixGeneration.Models
{
    /// <summary>
    /// Readonly binary matrix interface
    /// </summary>
    public interface IReadonlyMatrix
    {
        int Width { get; }
        int Height { get; }
        bool GetValue(int x, int y);
        bool[] GetFlatArrayClone();
    }
}