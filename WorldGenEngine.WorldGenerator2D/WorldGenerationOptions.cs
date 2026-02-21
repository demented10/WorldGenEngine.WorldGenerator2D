namespace WorldGenEngine.WorldGenerator2D
{
    public enum GenerationAlgorithmType
    {
        Random,
        PerlinNoise,
        CellularAutomata
    }
    public class WorldGenerationOptions
    {
        public int Seed { get; set; } = 0;
        public GenerationAlgorithmType GenerationAlgorithmType { get; set; } = GenerationAlgorithmType.Random;
    }
}