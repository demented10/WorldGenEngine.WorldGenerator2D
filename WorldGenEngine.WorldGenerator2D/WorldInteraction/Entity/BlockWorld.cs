using WorldGenEngine.WorldGenerator2D.Position;
using WorldGenEngine.WorldGenerator2D.WorldInteraction.Properties;

namespace WorldGenEngine.WorldGenerator2D.WorldInteraction.Entity
{
    /// <summary>
    /// Класс блока находящегося непосредственно в сгенерированном мире
    /// </summary>
    public class BlockWorld 
    {
        public IPoint Position { get; private set; }

        public Properties.WorldProperties WorldParent { get; private set; }
        public BlockProperties BlockMeta { get; private set; }

        public BlockWorld(Properties.WorldProperties world, IPoint position, BlockProperties blockMeta)
        {
            WorldParent = world;
            Position = position;
            BlockMeta = blockMeta;
        }

    }
}
