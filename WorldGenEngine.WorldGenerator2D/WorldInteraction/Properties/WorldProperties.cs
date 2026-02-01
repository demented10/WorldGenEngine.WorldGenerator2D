using WorldGenEngine.WorldGenerator2D.Position;

namespace WorldGenEngine.WorldGenerator2D.WorldInteraction.Properties
{
    /// <summary>
    /// Богатая модель мира, содержит основную информацию
    /// </summary>
    public class WorldProperties
    {
        const int CHUNK_SIZE = 64;
        public ISize2D WorldSizeInChunks { get; private set; }

        public WorldProperties()
        {
            WorldSizeInChunks = new Size2i(16, 16);
        }

        public WorldProperties(int worldWidthInChunks, int worldHeightInChunks)
        {
            WorldSizeInChunks = new Size2i(worldWidthInChunks, worldHeightInChunks);
        }
        public WorldProperties(ISize2D worldSizeInChunks)
        {
            WorldSizeInChunks = worldSizeInChunks;
        }

    }
}
