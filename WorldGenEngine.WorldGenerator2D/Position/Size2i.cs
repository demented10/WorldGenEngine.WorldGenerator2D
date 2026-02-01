namespace WorldGenEngine.WorldGenerator2D.Position { 
    public struct Size2i : ISize2D
    {
        public int Width { get; }
        public int Height { get; }


        public Size2i(int width, int height)
        {
            Width = width;
            Height = height;
        }

        float ISize2D.Width => Width;
        float ISize2D.Height => Height;

        public static implicit operator Size2f(Size2i size)
        {
            return new Size2f(size.Width, size.Height);
        }
        public static explicit operator Size2i(Size2f size)
        {
            return new Size2i((int)size.Width, (int)size.Height);
        }
    }
}
