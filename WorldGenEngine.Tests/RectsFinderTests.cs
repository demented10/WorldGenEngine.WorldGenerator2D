using Microsoft.Extensions.DependencyInjection;
using WorldGenEngine.Core.MatrixAlgorithms.Algorithms;

namespace WorldGenEngine.Tests;

public class RectsFinderTests
{
    protected IServiceProvider? getServiceProvider;

    [Fact]
    public void FindRects_EmptyMatrix_ReturnsEmptyList()
    {
        var services = getServiceProvider.GetRequiredService<IServiceCollection>();
        var sut = services.BuildServiceProvider().GetRequiredService<IRectsFinder>();
        bool[,] emptyMatrix = new bool[0, 0];
        var result = sut.FindRects(emptyMatrix);
        Assert.Empty(result);
    }
}

