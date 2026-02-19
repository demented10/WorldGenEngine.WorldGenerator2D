namespace WorldGenEngine.Core.MatrixGeneration.Generators
{

    /// <summary>
    /// Interface defining a binary matrix generator in flat array format
    /// </summary>
    public interface IBinaryMatrixGenerator
    {
        bool[] GenerateMatrix(int width, int height, int? seed = null);
    }

    public interface IHeightMapMatrixGenerator
    {
        float[] GenerateHeightMap(int width, int height, int? seed = null);
    }
}