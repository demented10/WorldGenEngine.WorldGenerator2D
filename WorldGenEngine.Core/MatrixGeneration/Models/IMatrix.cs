namespace WorldGenEngine.Core.MatrixGeneration.Models
{
    /// <summary>
    /// Editable binary matrix interface
    /// </summary>
    public interface IMatrix : IReadonlyMatrix
    {
        void SetValue(int x, int y, bool value);
    }
}