using Microsoft.Extensions.DependencyInjection;
using WorldGenEngine.Core;
using WorldGenEngine.Core.MatrixAlgorithms.Algorithms;
using WorldGenEngine.Core.MatrixAlgorithms.Models;
using WorldGenEngine.Core.MatrixGeneration.Generators;
using WorldGenEngine.Core.MatrixGeneration.Models;

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

    [Fact]
    public void FindRects_FindRectsWith2DimBool_ReturnsRectsList()
    {
        var sut = _serviceProvider.GetRequiredService<IRectsFinder>();
        bool[,] emptyMatrix =
        {
            { true, true, false }, 
            { true, true, false }, 
            { false, false, false }
        };
        var rects = sut.FindRects(emptyMatrix);
        
        Assert.NotEmpty(rects);
        Assert.Equal(new Rect(0,0,2,2), rects[0]);
        Assert.Single(rects);
    }

    [Fact]
    public void FindRects_FindRectsWithMatrix_ReturnsRectsList()
    {
        var sut = _serviceProvider.GetRequiredService<IRectsFinder>();
        
        bool[] emptyMatrix =
        {
             true, true, false,
             true, true, false ,
             false, false, false 
        };
        var matrix = new BinaryMatrix(3, 3, emptyMatrix);
        var rects = sut.FindRects(matrix);

        Assert.NotEmpty(rects);
        Assert.Equal(new Rect(0, 0, 2, 2), rects[0]);
        Assert.Single(rects);
    }

    [Fact]
    public void FindRects_RectsInRandomMatrix_ReturnsFalse()
    {
        var sut = _serviceProvider.GetRequiredService<IRectsFinder>();
        var generator = _serviceProvider.GetRequiredService<IBinaryMatrixGenerator>();
        var matrix = new BinaryMatrix(8,8, generator.GenerateMatrix(10, 10, 1));
        
        var rects = sut.FindRects(matrix);

        var result = matrix.GetFlatArrayClone();
        foreach (var rect in rects)
        {
            for (int x = rect.X; x < rect.X + rect.Width; x++)
            {
                for (int y = rect.Y; y < rect.Y + rect.Height; y++)
                {
                    result[matrix.Width * y + x] = false;
                }
            }
        }
        Assert.Empty(result);
    }
}

