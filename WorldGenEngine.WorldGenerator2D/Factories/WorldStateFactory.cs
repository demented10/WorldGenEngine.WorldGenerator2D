using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WorldGenEngine.WorldGenerator2D.Services.Generation;
using WorldGenEngine.WorldGenerator2D.Services.Statements;

namespace WorldGenEngine.WorldGenerator2D.Factories
{
    public static class WorldStateFactory
    {
        public static WorldState CreateWorldState(IChunkGenerationAlgo generationAlgo, IChunkStorageService chunkStorageService, WorldStateOptions options, WorldGenerationOptions generationOptions, IChunkCachingService chunkCachingService, ILogger<WorldState> logger = null)
        {
            return new WorldState(generationAlgo, chunkStorageService, options, generationOptions, chunkCachingService, logger);
        }
    }
}
