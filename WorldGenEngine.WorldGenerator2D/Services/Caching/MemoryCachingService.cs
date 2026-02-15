using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldGenEngine.WorldGenerator2D.Models;
using WorldGenEngine.WorldGenerator2D.Services.Statements;

namespace WorldGenEngine.WorldGenerator2D.Services.Caching
{
    internal class MemoryCachingService : IChunkCachingService
    {
        private readonly ConcurrentDictionary<ChunkPosition, ChunkData> _cache = new ConcurrentDictionary<ChunkPosition, ChunkData>();

        private void UnloadChunks()
        {

        }

        public void AddChunkToCache(ChunkPosition position, ChunkData chunkData)
        {
            _cache.TryAdd(position, chunkData);
        }

        public HashSet<ChunkPosition> GetExistingChunks()
        {
            return _cache.Keys.ToHashSet();
        }

        public Dictionary<ChunkPosition, ChunkData> GetLoadedChunks()
        {
            return _cache.ToDictionary(kpv => kpv.Key, kpv => kpv.Value);
        }

        public int GetCachedChunksCount()
        {
            return _cache.Count;
        }

        public bool IsChunkPositionCached(ChunkPosition position)
        {
            return _cache.ContainsKey(position);
        }

        public bool TryGetChunkData(ChunkPosition position, out ChunkData value)
        {
            if (_cache.TryGetValue(position, out var chunk))
            {
                value = chunk;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }
    }
}
