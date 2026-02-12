using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using WorldGenEngine.Core.GenerationMethods.Configs.PerlinNoise;

namespace WorldGenEngine.Core.GenerationMethods.Configs
{
    public static class PerlinNoiseConfigFactory
    {
        public static IPerlinNoiseConfig GetDefaultConfig() => PerlinNoiseConfig.Default();

        public static IPerlinNoiseConfig CreateConfig(float scale, float threshold, int octaves, float persistence, float lacunarity, float verticalBias, int seed)
        {
            return new PerlinNoiseConfig(scale, threshold, octaves, persistence, lacunarity, verticalBias, seed);
        }
    }
}
