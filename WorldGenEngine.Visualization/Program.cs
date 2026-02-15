using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;
using WorldGenEngine.Core.GenerationMethods.Configs;
using WorldGenEngine.Core.MatrixAlgorithms.Factory;
using WorldGenEngine.Core.MatrixGeneration.Factories;
using WorldGenEngine.Visualization;
using WorldGenEngine.Visualization.Visualisators.RaylibVisualize;
using WorldGenEngine.WorldGenerator2D;
using WorldGenEngine.WorldGenerator2D.Factories;
using WorldGenEngine.WorldGenerator2D.Models;
using WorldGenEngine.WorldGenerator2D.Services.Statements;


class Program
{
    static int w = 640, h = 640;


    public  static void  Main(string[] args)
    {
        var generator = MatrixGeneratorsFactory.CreatePerlinNoiseBinaryMatrixGenerator(PerlinNoiseConfigFactory.GetDefaultConfig());
        var rectFinder = RectFindersFactory.CreateRowDepthFinder();

        var services = new ServiceCollection();
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Debug);
        });

        services.AddWorldGeneration2D(worldGenerationOptions: options =>
        {
            options.GeneratorType = GeneratorType.Random;
            options.Seed = 42;
        }, options =>
        {
            options.AroundChunkRadius = 15;
            options.MaxLoadedChunks = 64;
        });
        using var serviceProvider = services.BuildServiceProvider();
        var generationServiceFactory = serviceProvider.GetRequiredService<IChunkGenerationServiceFactory>();
        var worldState = serviceProvider.GetRequiredService<WorldState>();

        IVisualisator visualizator = new RaylibWorldStateVisualizer(generationServiceFactory, worldState);
        visualizator.Visualize();



    }
}
