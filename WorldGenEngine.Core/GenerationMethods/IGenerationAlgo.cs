namespace WorldGenEngine.Core.GenerationMethods
{
    public interface IGenerationAlgo
    {
        bool[,] GenerateMap(int sizeX, int sizeY, int positionX = 0, int positionY = 0, int seed = 0);

        float[,] GenerateHeightMap(int sizeX, int sizeY, int positionX = 0, int positionY = 0, int seed = 0);

    }
}
