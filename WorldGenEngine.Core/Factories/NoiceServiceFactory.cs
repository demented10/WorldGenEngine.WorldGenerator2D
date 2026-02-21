using System;
using System.Collections.Generic;
using System.Text;
using WorldGenEngine.Core.Noise;
using WorldGenEngine.Core.Noise.Config;

namespace WorldGenEngine.Core.Factories
{
    public static class FractalNoiceServiceFactory
    {
        public static FractalNoiseService CreateFractalNoiseService(FractalNoiseServiceConfig config) => new FractalNoiseService(config.NoiseType switch
        {
            NoiseType.Perlin => new Noise.NoiseAlgorithms.PerlinNoise(),
            _ => throw new NotSupportedException($"Noise type {config.NoiseType} is not supported.")
        }, Microsoft.Extensions.Options.Options.Create(config));
    }
}
