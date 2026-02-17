using Microsoft.Extensions.DependencyInjection;
using WorldGenEngine.Core.GenerationMethods.Algorithms;
using WorldGenEngine.Core.MatrixAlgorithms.Algorithms;
using WorldGenEngine.Core.MatrixGeneration.Generators;
using WorldGenEngine.Core.MatrixGeneration.Models;

namespace WorldGenEngine.Core;

public enum RectFinderAlgorithmTypes
{
    RowDepth = 0,
}

public class RectFinderOptions
{
    public RectFinderAlgorithmTypes RectFinderAlgorithmType { get; set; } = default;
}

public static class CoreServiceCollectionExtensions
{
    public static IServiceCollection AddWorldGenEngine(this IServiceCollection services)
    {
        services.AddSingleton<IMatrix, BinaryMatrix>();
        services.AddSingleton<IRectsFinder, RowDepthRectFinder>();
        services.AddSingleton<IBinaryMatrixGenerator, RandomMatrixGenerator>();
        services.AddSingleton<GenAlgoToMatrixGenAdapter>();

        return services;
    }
}