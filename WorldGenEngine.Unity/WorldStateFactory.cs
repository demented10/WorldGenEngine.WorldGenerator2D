
using System.Data.Common;
using WorldGenEngine.WorldGenerator2D;
using WorldGenEngine.WorldGenerator2D.Algorithms;
using WorldGenEngine.WorldGenerator2D;
using WorldGenEngine.WorldGenerator2D.Services.Statements;

namespace WorldGenEngine.Unity
{

    public class WorldOptions
    {
        public WorldStateOptions StateOptions { get; set; } = new WorldStateOptions();
        public WorldGenerationOptions GenerationOptions { get; set; } = new WorldGenerationOptions();
        public NoiseChunkGeneratorConfig NoiseConfig { get; set; } = new NoiseChunkGeneratorConfig();
    }

    public static class WorldStateFactory
    {
        public static WorldState CreateWorldState(WorldOptions options)
        {
            return WorldGenerator2D.Factories.WorldStateFactory.CreateWorldState(
                generationAlgo: WorldGenerator2D.Factories.ChunkGenerationFactory.CreateNoiseChunkGenerator(
                    options.NoiseConfig),
                chunkStorageService: WorldGenerator2D.Factories.StorageServiceFactory.CreateStorageService(),
                options: options.StateOptions,
                generationOptions: options.GenerationOptions,
                chunkCachingService: WorldGenerator2D.Factories.CachingServiceFactory.CreateCachingService()

            );
        }
    }
}
