using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using WorldGenEngine.WorldGenerator2D.Algorithms;
using WorldGenEngine.WorldGenerator2D.Services.Generation;
using WorldGenEngine.WorldGenerator2D.Services.Statements;

namespace WorldGenEngine.WorldGenerator2D.Factories
{
    public enum GeneratorType
    {
        Random,
        PerlinNoise,
        CellularAutomata,
        
    }
    public interface IChunkGenerationServiceFactory
    {
        IChunkGenerationService Create(GeneratorType? type = null);
    }
    public class ChunkGenerationServiceFactory : IChunkGenerationServiceFactory
    {
        private readonly WorldGenerationOptions _options;

        public ChunkGenerationServiceFactory(IOptions<WorldGenerationOptions> options)
        {
            _options = options.Value;
        }

        public IChunkGenerationService Create(GeneratorType? type = null)
        {
            var actualType = type ?? _options.GeneratorType;
            IChunkGenerationAlgo chunkGenerationService = type switch
            {
                GeneratorType.Random => new RandomChunkGenerator(),
               //GeneratorType.PerlinNoise => new PerlinNoiseChunkGenerator(),
                _ => throw new NotSupportedException($"Generator type {actualType} is not supported.")
            };
            return new ChunkGenerationService(chunkGenerationService, _options.Seed);
        }

    }
}
