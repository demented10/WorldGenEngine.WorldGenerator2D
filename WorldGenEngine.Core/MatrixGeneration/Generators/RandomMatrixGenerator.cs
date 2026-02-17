namespace WorldGenEngine.Core.MatrixGeneration.Generators
{

    /// <summary>
    /// Implementation of a randomly filled binary matrix generator in flat format
    /// </summary>
    internal class RandomMatrixGenerator : IBinaryMatrixGenerator
    {
        public bool[] GenerateMatrix(int width, int height, int? seed = null)
        {
            Random rand = seed.HasValue ? new Random(seed.Value) : new Random();

            var matrix = new bool[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    matrix[y * width + x] = rand.Next(0, 2) % 2 == 0;
                }
            }

            return matrix;
        }
    }
}