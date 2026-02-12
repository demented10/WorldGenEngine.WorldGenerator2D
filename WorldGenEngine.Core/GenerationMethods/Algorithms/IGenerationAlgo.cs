namespace WorldGenEngine.Core.GenerationMethods.Algorithms
{
    public interface IGenerationAlgo
    {
        bool[,] GenerateMap(int sizeX, int sizeY);
    }
}
