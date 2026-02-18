using Microsoft.Extensions.DependencyInjection;
using WorldGenEngine.Core;
using WorldGenEngine.Core.MatrixAlgorithms.Algorithms;

namespace WorldGenEngine.Tests;

public class RectsFinderTests
{

    protected IServiceProvider _serviceProvider;

    public RectsFinderTests()
    {

            var services = new ServiceCollection();
            services.AddWorldGenEngine();
            _serviceProvider = services.BuildServiceProvider();

    }

    [Fact]
    public void FindRects_EmptyMatrix_ReturnsEmptyList()
    {

        var sut = _serviceProvider.GetRequiredService<IRectsFinder>();
        bool[,] emptyMatrix = new bool[0, 0];
        var result = sut.FindRects(emptyMatrix);
        Assert.Empty(result);
    }

}

