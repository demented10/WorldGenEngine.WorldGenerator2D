using System;
using System.Collections.Generic;
using System.Text;
using WorldGenEngine.Core.Noise.Config;
using WorldGenEngine.WorldGenerator2D.Algorithms;
using WorldGenEngine.WorldGenerator2D.Services.Generation;

namespace WorldGenEngine.WorldGenerator2D.Factories
{

    public static class ChunkGenerationFactory
    {
        public static IChunkGenerationAlgo CreateRandomChunkGenerator()
        {
                return new Algorithms.RandomChunkGenerator();
        }
        public static IChunkGenerationAlgo CreateNoiseChunkGenerator(NoiseChunkGeneratorConfig config)
        {
            var fractalNoiseService = Core.Factories.FractalNoiceServiceFactory.CreateFractalNoiseService(config.FractalNoiseConfig);
            return new PerlinNoiseChunkGenerator(fractalNoiseService, config);
        }
    }
}
