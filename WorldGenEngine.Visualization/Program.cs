using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WorldGenEngine.Core;
using WorldGenEngine.Visualization.Visualisators.RaylibVisualize;
using WorldGenEngine.WorldGenerator2D;
using WorldGenEngine.WorldGenerator2D.Services.Statements;

namespace WorldGenEngine.Visualization
{

    class Program
    {
        public static void Main(string[] args)
        {

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

            IVisualisator visualizator = new RaylibWorldStateVisualizer(worldState);
            visualizator.Visualize();



        }
    }
}