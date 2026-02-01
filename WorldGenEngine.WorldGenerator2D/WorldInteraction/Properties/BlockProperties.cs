namespace WorldGenEngine.WorldGenerator2D.WorldInteraction.Properties
{
    /// <summary>
    /// Информация о блоке в мире
    /// </summary>
    public class BlockProperties
    {
        public enum BlockType
        {
            Empty,
            Soft,
            Hard,
        }
        public BlockType Type { get; set; }


    }
}
