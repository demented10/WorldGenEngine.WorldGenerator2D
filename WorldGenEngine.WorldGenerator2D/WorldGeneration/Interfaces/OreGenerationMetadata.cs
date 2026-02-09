namespace WorldGenEngine.WorldGenerator2D.WorldGeneration.Interfaces
{

    public readonly struct OreGenerationMetadata(
        IOre ore,
        int minOreGenerationY,
        int maxOreGenerationY,
        int maxOreGenerationWidth,
        int minOreGenerationWidth,
        int minOreGenerationHeight,
        int maxOreGenerationHeight)
    {
        public IOre Ore { get; } = ore;

        /// <summary>
        /// Минимальная Y координата генерации руды
        /// </summary>
        public int MinOreGenerationY { get; } = minOreGenerationY;

        /// <summary>
        /// Максимальная Y координата генерации руды
        /// </summary>
        public int MaxOreGenerationY { get; } = maxOreGenerationY;

        /// <summary>
        /// Максимальная ширина генерации руды
        /// </summary>
        public int MaxOreGenerationWidth { get; } = maxOreGenerationWidth;

        /// <summary>
        /// Минимальная ширина генерации руды
        /// </summary>
        public int MinOreGenerationWidth { get; } = minOreGenerationWidth;

        /// <summary>
        /// Минимальная высота генерации руды
        /// </summary>
        public int MinOreGenerationHeight { get; } = minOreGenerationHeight;

        /// <summary>
        /// Максимальная высота генерации руды
        /// </summary>
        public int MaxOreGenerationHeight { get; } = maxOreGenerationHeight;
    }
}