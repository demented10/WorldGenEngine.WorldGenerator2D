using System;
using System.Collections.Generic;
using System.Text;
using WorldGenEngine.Core.Noise;
using WorldGenEngine.Core.Noise.NoiseAlgorithms;

namespace WorldGenEngine.Core.Factories
{
    public enum NoiseType
    {
        Perlin,
        Simplex
    }   
    public static class NoiseFactory
    {
        public static INoise CreateNoise(NoiseType type)
            => type switch
            {
                NoiseType.Perlin => new PerlinNoise(),
                _ => throw new ArgumentException($"Unsupported noise type: {type}")
            };

    }
}
