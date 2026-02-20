using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;
using WorldGenEngine.Core;
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
        
        var services = new ServiceCollection();
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Debug);
        });
        services.AddWorldGenEngine(config =>
        {
            config.Lacunarity = 1.25f;
            config.Octaves = 2;
            config.Persistence = 0.45f;
        });
        services.AddWorldGeneration2D(worldGenerationOptions: options =>
        {
            options.GeneratorType = GeneratorType.Random;
            options.Seed = 5;
        }, options =>
        {
            options.AroundChunkRadius = 5;
            options.MaxLoadedChunks = 100;
        }, config =>
        {
            config.Scale = 0.25f;
            config.Threshold = -0.01f;
        });

        using var serviceProvider = services.BuildServiceProvider();
        var worldState = serviceProvider.GetRequiredService<WorldState>();

        IVisualisator visualizator = new RaylibWorldStateVisualizer(worldState);
        visualizator.Visualize();



    }
}
