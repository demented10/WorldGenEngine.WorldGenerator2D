namespace WorldGenEngine.WorldGenerator2D.Position
{
    public struct Size2f : ISize2D
    {
        public float Width { get; set; }
        public float Height { get; set; }
        public Size2f(float width, float height)
        {
            Width = width;
            Height = height;
        }
        float ISize2D.Width => Width;
        float ISize2D.Height => Height;

    }
}
