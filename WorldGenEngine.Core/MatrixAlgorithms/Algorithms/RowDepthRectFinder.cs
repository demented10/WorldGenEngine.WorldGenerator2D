using WorldGenEngine.Core.MatrixAlgorithms.Models;
using WorldGenEngine.Core.MatrixGeneration.Models;
using WorldGenEngine.Core.Utils;

namespace WorldGenEngine.Core.MatrixAlgorithms.Algorithms
{
    /// <summary>
    /// Implements an algorithm for partitioning a binary matrix into rectangles. It works row-by-row with depth-first traversal.
    /// </summary>
    class RowDepthRectFinder : IRectsFinder
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
        private static void ClearRectInMatrix(bool[,] matrix, Rect rect)
        {
            for (int xI = 0; xI < rect.Width; xI++)
            {
                for (int yI = 0; yI < rect.Height; yI++)
                {
                    matrix[xI + rect.X, yI + rect.Y] = false;
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

        private static Rect FindLocalRect(int x, int y, bool[,] matrix)
        {
            var curX = x;

            var startX = x;
            var startY = y;

            var matrixHeight = matrix.GetLength(1);
            var matrixWidth = matrix.GetLength(0);

            var rectWidth = 0;
            int rectHeight = matrixHeight;

            //We start a loop to iterate over the line
            while (curX < matrixWidth &&
                   matrix[curX, startY])
            {
                var curY = startY;
                //We launch a passage into the depths
                while (curY < matrixHeight && matrix[curX, curY])
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
                        if (rect.Width > 0) x += rect.Width - 1;
                    }
                }
            }

            return rects;
        }


        public List<Rect> FindRects(bool[,] matrix)
        {
            var matrixWidth = matrix.GetLength(0);
            var matrixHeight = matrix.GetLength(1);

            var m = (bool[,])matrix.Clone();
            var rects = new List<Rect>();

            for (int y = 0; y < matrixHeight; y++)
            {
                for (int x = 0; x < matrixWidth; x++)
                {
                    //Finding the first true occurrence
                    if (matrix[x, y])
                    {
                        Rect rect = FindLocalRect(x, y, m);
                        rects.Add(rect);
                        ClearRectInMatrix(m, rect);
                        if(rect.Width>0)x += rect.Width - 1;
                    }
                }
            }

            return rects;
        }
    }
}