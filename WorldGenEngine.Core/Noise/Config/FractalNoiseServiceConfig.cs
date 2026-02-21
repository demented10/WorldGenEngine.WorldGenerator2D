using WorldGenEngine.Core.Factories;

namespace WorldGenEngine.Core.Noise.Config;

public sealed class FractalNoiseServiceConfig
{
    public float Lacunarity { get; set; } = 2.0f;
    public int Octaves { get; set;  } = 4;
    public float Persistence { get; set; } = 0.5f;

    public NoiseType NoiseType { get; set; } = NoiseType.Perlin;
}