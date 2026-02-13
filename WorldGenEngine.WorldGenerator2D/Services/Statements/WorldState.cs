using System;
using System.Collections.Generic;
using WorldGenEngine.WorldGenerator2D.Models;

namespace WorldGenEngine.WorldGenerator2D.Services.Statements
{
    [Serializable]
    internal class WorldState
    {
        private readonly Dictionary<ChunkPosition, ChunkState> _loadedChunks;
        private readonly HashSet<ChunkPosition> _existingChunks;
        private readonly IChunkStorageService _chunkStorageService;
        private readonly IChunkGenerationService _chunkGenerationService;

        public static int MaxLoadedChunks = 100; //TODO перенести в конфиг
        public static int AroundChunksRadius = 2; //TODO перенести в конфиг

        public WorldState(IChunkGenerationService chunkGenerationService, IChunkStorageService chunkStorageService)
        {
            _chunkGenerationService = chunkGenerationService;
            _chunkStorageService = chunkStorageService;
            _loadedChunks = new Dictionary<ChunkPosition, ChunkState>();
            _existingChunks = _chunkStorageService.GetGeneratedChunkPositions();
        }

        /// <summary>
        /// Saves chunks states to storage
        /// </summary>
        /// <param name="newStates"></param>
        public void SaveChunks(ChunkState[] newStates)
        {

        }

        /// <summary>
        /// Check is chunk loaded in cache
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool CheckChunkIsLoaded(ChunkPosition position)
        {
            return _loadedChunks.ContainsKey(position);
        }
        /// <summary>
        /// Check is chunk exists in storage
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool CheckChunkExistsInStorage(ChunkPosition position)
        {
            return _existingChunks.Contains(position);
        }



        /// <summary>
        /// Returns a box shaped array of chunk positions around the given chunk position.
        /// </summary>
        /// <param name="aroundChunk"></param>
        /// <returns></returns>
        private IEnumerable<ChunkPosition> GetAroundPositions(ChunkPosition aroundChunk)
        {
            for(int x = aroundChunk.ChunkXPos - AroundChunksRadius; x <= aroundChunk.ChunkXPos + AroundChunksRadius; x++)
            {
                for (int y = aroundChunk.ChunkYPos - AroundChunksRadius; y <= aroundChunk.ChunkYPos + AroundChunksRadius; y++)
                {
                    yield return new ChunkPosition(x, y);
                }
            }
        }

        /// <summary>
        /// Returns a list of chunks states around the given chunk position.
        /// If the chunk is not loaded in cache, it will be loaded from storage if it exists,
        /// otherwise it will be generated and saved to storage.
        /// </summary>
        /// <param name="aroundChunk"></param>
        /// <returns></returns>
        public List<ChunkState> GetChunksStates(ChunkPosition aroundChunk)
        {
            List<ChunkState> states = new List<ChunkState>();

            foreach (var chunkPosition in GetAroundPositions(aroundChunk))
            {
                if (CheckChunkIsLoaded(chunkPosition))
                {
                    if (_loadedChunks.TryGetValue(chunkPosition, out var value))
                    {
                        states.Add(value);
                    }
                }
            }
            
            return states;

        }

        private void LoadChunksFromStorage(ChunkPosition[] positions)
        {

        }

    }
}