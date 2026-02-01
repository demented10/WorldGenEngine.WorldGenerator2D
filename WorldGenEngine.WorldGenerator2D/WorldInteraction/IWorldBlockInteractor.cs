using WorldGenEngine.WorldGenerator2D.Position;
using WorldGenEngine.WorldGenerator2D.WorldInteraction.Entity;
using WorldGenEngine.WorldGenerator2D.WorldInteraction.Properties;

namespace WorldGenEngine.WorldGenerator2D.WorldInteraction
{
    public interface IWorldBlockInteractor
    {
        BlockWorld GetBlock(IPoint position);
        void SetBlock(IPoint position, BlockProperties blockMeta);

    }
}
