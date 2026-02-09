using WorldGenEngine.WorldGenerator2D.WorldInteraction.Entity;

namespace WorldGenEngine.WorldGenerator2D.WorldInteraction
{
    public readonly struct WorldProperties(BlockWorld[] worldBlocks)
    {
        public BlockWorld[] WorldBlocks { get; } = worldBlocks;
    }
}
