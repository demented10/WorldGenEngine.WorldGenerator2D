using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WorldGenEngine.WorldGenerator2D.Algorithms;
using WorldGenEngine.WorldGenerator2D.Services.Caching;
using WorldGenEngine.WorldGenerator2D.Services.Generation;
using WorldGenEngine.WorldGenerator2D.Services.Statements;
using WorldGenEngine.WorldGenerator2D.Services.Storage;

namespace WorldGenEngine.WorldGenerator2D
{
    public static class WorldGenerationServiceCollectionExtensions
    {
        public static IServiceCollection AddWorldGeneration2D(this IServiceCollection services, Action<WorldGenerationOptions> worldGenerationOptions = null, Action<WorldStateOptions> worldStateOptions = null, Action<NoiseChunkGeneratorConfig> chunkGenerationConfig = null)
        {
            var generationOptions = new WorldGenerationOptions();
            worldGenerationOptions?.Invoke(generationOptions);
            services.AddSingleton(Options.Create(generationOptions));
                
            var stateOptions = new WorldStateOptions();
            worldStateOptions?.Invoke(stateOptions);
            services.AddSingleton(Options.Create(stateOptions));

            var chunkGenerationOptions = new NoiseChunkGeneratorConfig();
            chunkGenerationConfig?.Invoke(chunkGenerationOptions);
            services.AddSingleton(Options.Create(chunkGenerationOptions));


            services.AddSingleton<IChunkGenerationAlgo, PerlinNoiseChunkGenerator>();
            services.AddSingleton<IChunkStorageService, InMemoryChunkStorage>();
            services.AddSingleton<IChunkCachingService, MemoryCachingService>();

            services.AddScoped<WorldState>();

            return services;
        }
    }
}
