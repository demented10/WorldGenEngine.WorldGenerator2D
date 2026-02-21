using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WorldGenEngine.Core;
using WorldGenEngine.Core.Noise.Config;
using WorldGenEngine.Visualization.Visualisators.RaylibVisualize;
using WorldGenEngine.WorldGenerator2D;
using WorldGenEngine.WorldGenerator2D.Algorithms;
using WorldGenEngine.WorldGenerator2D.Services.Statements;

namespace WorldGenEngine.Visualization
{

    class Program
    {
        public static void Main(string[] args)
        {
            var fractalNoiseConfig = new FractalNoiseServiceConfig
            {
                Lacunarity = 1.25f,
                Octaves = 4,
                Persistence = 0.45f
            };

            var chunkGenerationConfig = new NoiseChunkGeneratorConfig
            {
                FractalNoiseConfig = fractalNoiseConfig,
                Scale = 0.1f,
                Threshold = -0.1f
            };

            var worldGenerationOptions = new WorldGenerationOptions
            {
                Seed = 5
            };

            var worldStateOptions = new WorldStateOptions
            {
                AroundChunkRadius = 5,
                MaxLoadedChunks = 100
            };


            var services = new ServiceCollection();
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Debug);
            });
            services.AddWorldGenEngine(config =>
            {
                config.Lacunarity = 1.25f;
                config.Octaves = 4;
                config.Persistence = 0.45f;
            });
            services.AddWorldGeneration2D(worldGenerationOptions: options => { options.Seed = 5; }, options =>
            {
                options.AroundChunkRadius = 5;
                options.MaxLoadedChunks = 100;
            }, config =>
            {
                config.Scale = 0.1f;
                config.Threshold = -0.1f;
            });

            using var serviceProvider = services.BuildServiceProvider();
            var worldState = serviceProvider.GetRequiredService<WorldState>();

            var options = new Unity.WorldOptions
            {
                GenerationOptions = worldGenerationOptions,
                StateOptions = worldStateOptions,
                NoiseConfig = chunkGenerationConfig
            };
            var worldStateFactoried = Unity.WorldStateFactory.CreateWorldState(options);

            IVisualisator visualizator = new RaylibWorldStateVisualizer(worldStateFactoried);
            visualizator.Visualize();





        }
    }
}