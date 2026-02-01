namespace WorldGenEngine.WorldGenerator2D.CaveGenerator
{
    public interface IMapGenerator
    {
        bool[,] GenerateMap(int sizeX, int sizeY);
    }
}
