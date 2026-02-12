namespace WorldGenEngine.Core.Utils
{

    /// <summary>
    /// Helper class for working with a flat array as a matrix
    /// </summary>
    public static class MatrixUtils
    {
        /// <summary>
        /// Get a value by coordinates in a flat array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="width">Line width</param>
        /// <param name="x">X-axis coordinate</param>
        /// <param name="y">Y-axis coordinate</param>
        /// <param name="matrix">Flat array for working as a matrix</param>
        /// <returns></returns>
        public static T? GetValueInFlatMatrix<T>(int width, int x, int y, T[]? matrix)
        {
            var index = Compare2DimToIndex(width, x, y);
            if (matrix == null || index >= matrix.Length) return default;
            return matrix[index];
        }

        /// <summary>
        /// Converting a two-dimensional coordinate to an index
        /// </summary>
        /// <param name="width">Line width</param>
        /// <param name="x">X-axis coordinate</param>
        /// <param name="y">Y-axis coordinate</param>
        /// <returns></returns>
        public static int Compare2DimToIndex(int width, int x, int y)
        {
            return y * width + x;
        }

    }
}