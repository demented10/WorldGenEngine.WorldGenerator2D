using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using WorldGenEngine.WorldGenerator2D.Models;

namespace WorldGenEngine.WorldGenerator2D.Services.Statements
{
    public interface IChunkCachingService
    {
        void AddChunkToCache(ChunkPosition position, ChunkData chunkData);
        HashSet<ChunkPosition> GetExistingChunks();
        Dictionary<ChunkPosition, ChunkData> GetLoadedChunks();

        int GetCachedChunksCount();
        bool IsChunkPositionCached(ChunkPosition position);
        bool TryGetChunkData(ChunkPosition position, out ChunkData value);

    }
}