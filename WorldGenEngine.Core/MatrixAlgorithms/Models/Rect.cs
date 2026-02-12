namespace WorldGenEngine.Core.MatrixAlgorithms.Models
{
    /// <summary>
    /// Structure realise rect NxM
    /// </summary>
    public struct Rect
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        /// <summary>
        /// Structure realise rect NxM
        /// </summary>
        /// <param name="x">Start X axis point</param>
        /// <param name="y">Start Y axis point</param>
        /// <param name="width">size for axis X</param>
        /// <param name="height">size for axis Y</param>
        public Rect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public int XMax => X + Width;
        public int YMax => Y + Height;


        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, Width: {Width}, Height: {Height}";
        }
    }
}