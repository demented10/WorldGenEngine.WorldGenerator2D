using System.Collections.Generic;
using WorldGenEngine.WorldGenerator2D.Models;

namespace WorldGenEngine.WorldGenerator2D.Services.Statements
{
    public interface IChunkStorageService
    {
        /// <summary>
        /// Use to save changed or generated chunks to the storage. This method will be called by the world state service when it needs to save the state of chunks, such as when a chunk is modified or when the world generator needs to persist the current state of the world. The implementation of this method should handle the actual storage mechanism, whether it's saving to a file, a database, or any other form of persistent storage. 
        /// </summary>
        /// <param name="chunksData"></param>
        public void SaveChunkState(Dictionary<ChunkPosition, ChunkData> chunksData);
        /// <summary>
        /// Use to load chunks from the storage. This method will be called by the world state service when it needs to load the state of chunks, such as when a chunk is being generated or when the world generator needs to retrieve the current state of the world. The implementation of this method should handle the actual retrieval mechanism, whether it's loading from a file, a database, or any other form of persistent storage. The method should return a dictionary containing the chunk positions and their corresponding chunk data for the requested chunk positions.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Dictionary<ChunkPosition,ChunkData> LoadChunks(ChunkPosition[] position);
        /// <summary>
        /// Use to get the positions of all generated chunks. This method will be called by the world state service when it needs to determine which chunks have been generated and are currently stored in the storage. The implementation of this method should return a hash set containing the positions of all generated chunks, allowing the world generator to efficiently check if a chunk has already been generated or if it needs to be created. This can help optimize the chunk generation process and ensure that the world state is accurately maintained.
        /// </summary>
        /// <returns></returns>
        public HashSet<ChunkPosition> GetGeneratedChunkPositions();
    }
}