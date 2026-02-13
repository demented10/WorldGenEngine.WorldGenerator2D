using WorldGenEngine.WorldGenerator2D.Models;

namespace WorldGenEngine.WorldGenerator2D.Services.Generation
{
    internal interface IChunkGenerationAlgo
    {
        ChunkData GenerateChunk(ChunkPosition position, int seed = 0);
    }
}