using System.Runtime.CompilerServices;

namespace WorldGenEngine.Core.MatrixGeneration.Models
{
    /// <summary>
    /// Model implementing a binary matrix
    /// </summary>
    public readonly struct BinaryMatrix : IMatrix
    {
        /// <summary>
        /// Model implementing a binary matrix
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="matrix"></param>
        public BinaryMatrix(int width, int height, bool[] matrix)
        {
            Width = width;
            Height = height;
            Matrix = matrix;
        }

        public int Width { get; }
        public int Height { get; }

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

        public bool[] Matrix { get; }

        public int Length => Matrix.Length;
        public bool this[int index] => Matrix[index];
    }
}