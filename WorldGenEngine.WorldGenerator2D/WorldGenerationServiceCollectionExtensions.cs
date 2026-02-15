using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WorldGenEngine.WorldGenerator2D.Algorithms;
using WorldGenEngine.WorldGenerator2D.Factories;
using WorldGenEngine.WorldGenerator2D.Services.Caching;
using WorldGenEngine.WorldGenerator2D.Services.Generation;
using WorldGenEngine.WorldGenerator2D.Services.Statements;
using WorldGenEngine.WorldGenerator2D.Services.Storage;

namespace WorldGenEngine.WorldGenerator2D
{
    
    public class WorldGenerationOptions
    {
        public GeneratorType GeneratorType { get; set; } = GeneratorType.Random;
        public int Seed { get; set; } = 0;
    }

    public class WorldStateOptions
    {
        public int MaxLoadedChunks { get; set; } = 100;
        public int AroundChunkRadius { get; set; } = 3;
    }
    public static class WorldGenerationServiceCollectionExtensions
    {
        public static IServiceCollection AddWorldGeneration2D(this IServiceCollection services, Action<WorldGenerationOptions> worldGenerationOptions = null, Action<WorldStateOptions> worldStateOptions = null)
        {
            var generationOptions = new WorldGenerationOptions();
            worldGenerationOptions?.Invoke(generationOptions);
            services.AddSingleton(Options.Create(generationOptions));
                
            var stateOptions = new WorldStateOptions();
            worldStateOptions?.Invoke(stateOptions);
            services.AddSingleton(Options.Create(stateOptions));

            services.AddSingleton<IChunkGenerationServiceFactory, ChunkGenerationServiceFactory>();
            services.AddSingleton<IChunkStorageService, InMemoryChunkStorage>();
            services.AddSingleton<IChunkCachingService, MemoryCachingService>();

            services.AddScoped<WorldState>();

            return services;
        }
    }
}
