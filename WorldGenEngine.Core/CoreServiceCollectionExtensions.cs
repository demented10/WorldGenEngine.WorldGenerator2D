using Microsoft.Extensions.DependencyInjection;
using WorldGenEngine.Core.MatrixAlgorithms.Algorithms;
using WorldGenEngine.Core.MatrixGeneration.Generators;
using WorldGenEngine.Core.MatrixGeneration.Models;
using WorldGenEngine.Core.Noise;
using WorldGenEngine.Core.Noise.Config;
using WorldGenEngine.Core.Noise.NoiseAlgorithms;

namespace WorldGenEngine.Core;

public enum RectFinderAlgorithmTypes
{
    RowDepth = 0,
    PerlinNoise = 1,
}

public class RectFinderOptions
{
    public RectFinderAlgorithmTypes RectFinderAlgorithmType { get; set; } = default;
}

public static class CoreServiceCollectionExtensions
{
    public static IServiceCollection AddWorldGenEngine(this IServiceCollection services,
        Action<FractalNoiseServiceConfig> fractalNoiseConfig = null)

    {

        var fractalOptions = new FractalNoiseServiceConfig();
        fractalNoiseConfig?.Invoke(fractalOptions);
        services.AddSingleton(fractalOptions);

        services.AddScoped<IMatrix, BinaryMatrix>();
        services.AddSingleton<IRectsFinder, RowDepthRectFinder>();
        services.AddSingleton<IBinaryMatrixGenerator, RandomMatrixGenerator>();
        services.AddSingleton<IBinaryMatrixGenerator, PerlinNoiseMatrixGenerator>();
        services.AddSingleton<INoise, PerlinNoise>();
        services.AddSingleton<FractalNoiseService>(sp =>
        {
            var noise = sp.GetRequiredService<INoise>();
            var options = sp.GetRequiredService<FractalNoiseServiceConfig>();
            return new FractalNoiseService(noise, options);
        });

        return services;
    }
}