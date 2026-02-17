using WorldGenEngine.Core.MatrixGeneration.Factories;

namespace WorldGenEngine.Tests;

public class MatrixGenerationTest
{
    [Fact]
    public void Generate_Random_Binary_Matrix()
    {
        var binaryMatrix = MatrixGeneratorsFactory.CreateDefault().GenerateMatrix(10, 10);

        Assert.Contains(binaryMatrix, v => v);
        Assert.Contains(binaryMatrix, v => v == false);
    }
}