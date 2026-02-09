using WorldGenEngine.Core.MatrixAlgorithms.Models;
using WorldGenEngine.Core.MatrixGeneration.Models;

namespace WorldGenEngine.Core.MatrixAlgorithms.Algorithms;


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


/// <summary>
/// Implements an algorithm for partitioning a binary matrix into rectangles. It works row-by-row with depth-first traversal.
/// </summary>
class PolygonFinder : IRectsFinder
{
    /// <summary>
    /// Mark as false a rectangular region of the matrix
    /// </summary>
    /// <param name="matrixWidth"></param>
    /// <param name="matrix"></param>
    /// <param name="rect"></param>
    private static void ClearRectInMatrix(int matrixWidth, bool[] matrix, Rect rect)
    {
        for (int xI = 0; xI < rect.Width; xI++)
        {
            for (int yI = 0; yI < rect.Height; yI++)
            {
                matrix[MatrixUtils.Compare2DimToIndex(matrixWidth, xI + rect.X, yI + rect.Y)] = false;
            }
        }
    }

    /// <summary>
    /// Find a rectangle that includes all true occurrences
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="matrixWidth"></param>
    /// <param name="matrixHeight"></param>
    /// <param name="matrix"></param>
    /// <returns></returns>
    private static Rect FindLocalRect(int x, int y, int matrixWidth, int matrixHeight, bool[] matrix)
    {
        var curX = x;

        var startX = x;
        var startY = y;

        var rectWidth = 0;
        int rectHeight = matrixHeight;

        //We start a loop to iterate over the line
        while (curX < matrixWidth &&
               matrix[MatrixUtils.Compare2DimToIndex(matrixWidth, curX, startY)])
        {
            var curY = startY;
            //We launch a passage into the depths
            while (curY < matrixHeight && matrix[MatrixUtils.Compare2DimToIndex(matrixWidth, curX, curY)])
            {
                var curHeight = curY - startY;

                //If the current height is equal to the minimum column height, then we exit the column aisle
                if (curHeight >= rectHeight)
                {
                    break;
                }

                curY++;
            }

            var columnHeight = curY - startY;
            if (columnHeight <= rectHeight) rectHeight = columnHeight;
            rectWidth++;
            curX++;
        }

        Rect rect = new Rect(startX, startY, rectWidth, rectHeight);
        return rect;
    }

    /// <summary>
    /// Row - depth based algo for rect finding
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public List<Rect> FindRects(IMatrix matrix)
    {
        var matrixWidth = matrix.Width;
        var matrixHeight = matrix.Height;

        var m = matrix.GetFlatArrayClone();
        List<Rect> rects = new List<Rect>();


        for (int y = 0; y < matrix.Height; y++)
        {
            for (int x = 0; x < matrix.Width; x++)
            {

                //Finding the first true occurrence
                if (MatrixUtils.GetValueInFlatMatrix(matrixWidth, x, y, m))
                {
                    Rect rect = FindLocalRect(x, y, matrixWidth, matrixHeight, m);
                    rects.Add(rect);
                    ClearRectInMatrix(matrixWidth, m, rect);
                    x += rect.Width-1;
                }
            }
        }
        return rects;
    }

    
}