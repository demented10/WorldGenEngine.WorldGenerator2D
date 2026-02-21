using WorldGenEngine.Core.Noise.Config;

namespace WorldGenEngine.WorldGenerator2D.Algorithms
{
    public class NoiseChunkGeneratorConfig
    {
        public float Scale { get; set; }= 0.025f;
        public float Threshold { get; set; } = 0.5f;

        public FractalNoiseServiceConfig FractalNoiseConfig { get; set; } = new FractalNoiseServiceConfig();
    }
}