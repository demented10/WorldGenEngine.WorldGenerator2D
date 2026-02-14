using WorldGenEngine.WorldGenerator2D.Models;

namespace WorldGenEngine.WorldGenerator2D.Services.Statements
{
    public interface IChunkGenerationService
    {
        /// <summary>
        /// Uses to generate chunk data for the given chunk positions. This method will be called by the world state service when it needs to generate new chunks for the world. The implementation of this method should handle the actual generation logic, which may involve using a specific algorithm or method to create the chunk data based on the provided chunk positions. The method should return an array of chunk data corresponding to the input chunk positions, allowing the world generator to efficiently create and manage the chunks in the world state.
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        ChunkData[] GenerateChunksData (ChunkPosition[] positions);

        ChunkData GenerateChunkData(ChunkPosition position);
    }
}