
namespace WorldGenEngine.Core.GenerationMethods.Configs.PerlinNoise
{
#if NET10_0_OR_GREATER
    internal record PerlinNoiseConfig(float Scale, float Threshold, int Octaves, float Persistence, float Lacunarity, float VerticalBias, int Seed) : IPerlinNoiseConfig
    {
        public static PerlinNoiseConfig Default() => new(0.025f, 0.45f, 8, 0.8f, 1.1f, 0.25f, 100);
    }
#else
    internal struct PerlinNoiseConfig : IPerlinNoiseConfig
    {
        public float Scale { get; }
        public float Threshold { get; }
        public int Octaves { get; }
        public float Persistence { get; }
        public float Lacunarity { get; }
        public float VerticalBias { get; }
        public int Seed { get; }

        public PerlinNoiseConfig(float scale, float threshold, int octaves, float persistence, float lacunarity, float verticalBias, int seed)
        {
            Scale = scale;
            Threshold = threshold;
            Octaves = octaves;
            Persistence = persistence;
            Lacunarity = lacunarity;
            VerticalBias = verticalBias;
            Seed = seed;
        }

        public static PerlinNoiseConfig Default() => new(0.025f, 0.45f, 8, 0.8f, 1.1f, 0.25f, 100);
    }
#endif
}
