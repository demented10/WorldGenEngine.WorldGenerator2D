using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Microsoft.Extensions.Options;
using WorldGenEngine.WorldGenerator2D.Factories;
using WorldGenEngine.WorldGenerator2D.Models;

namespace WorldGenEngine.WorldGenerator2D.Services.Statements
{
    [Serializable]
    internal class WorldState
    {
        private readonly Dictionary<ChunkPosition, ChunkState> _loadedChunks;
        private readonly HashSet<ChunkPosition> _existingChunks;
        private readonly IChunkStorageService _chunkStorageService;
        private readonly IChunkGenerationServiceFactory _chunkGenerationServiceFactory;
        private readonly WorldStateOptions _options;

        public WorldState(IChunkGenerationServiceFactory chunkGenerationServiceFactory, IChunkStorageService chunkStorageService, IOptions<WorldStateOptions> options)
        {
            _chunkGenerationServiceFactory = chunkGenerationServiceFactory;
            _chunkStorageService = chunkStorageService;
            _options = options.Value;
            _loadedChunks = new Dictionary<ChunkPosition, ChunkState>();
            _existingChunks = _chunkStorageService.GetStoredChunkPositions();
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
            for(int x = aroundChunk.ChunkXPos - _options.AroundChunkRadius; x <= aroundChunk.ChunkXPos + _options.AroundChunkRadius; x++)
            {
                for (int y = aroundChunk.ChunkYPos - _options.AroundChunkRadius; y <= aroundChunk.ChunkYPos + _options.AroundChunkRadius; y++)
                {
                    yield return new ChunkPosition(x, y);
                }
            }
        }
        

        /// <summary>
        /// Loads chunks states from storage
        /// </summary>
        /// <param name="positions"></param>
        private bool TryLoadChunkFromStorage(ChunkPosition position, out ChunkState state)
        {
            try
            {
                state = new ChunkState(position,_chunkStorageService.LoadChunkData(position));
                return true;
            }
            catch (Exception exception)
            {
                state = default;
                return false;
            }
            
        }

        private bool TryGenerateChunk(ChunkPosition position, out ChunkState state)
        {
            try
            {
                state = new ChunkState(position, _chunkGenerationServiceFactory.Create().GenerateChunkData(position));
                return true;
            }
            catch (Exception exception)
            {
                state = default;
                return false;
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
                //Check if chunk is loaded in cache
                if (CheckChunkIsLoaded(chunkPosition))
                {
                    if (_loadedChunks.TryGetValue(chunkPosition, out var value))
                    {
                        states.Add(value);
                    }
                    else
                    {
                        continue; //TODO обработать ситуацию, когда TryGetValue возвращает false, хотя CheckChunkIsLoaded возвращает true
                    }
                }
                else if(CheckChunkExistsInStorage(chunkPosition)) //Check if chunk exists in storage
                {
                    if (TryLoadChunkFromStorage(chunkPosition, out var state))
                    {
                        states.Add(state);
                        _loadedChunks[state.Position] = state;
                    }
                    else
                    {
                        continue; //TODO обработать ситуацию, когда TryGetChunkData возвращает false, хотя CheckChunkExistsInStorage возвращает true
                    }
                }
                else //Generate chunk and save to storage
                {
                    if (TryGenerateChunk(chunkPosition, out var state))
                    {
                        states.Add(state);
                        AddChunkToCache(state.Position, state);
                    }
                    else
                    {
                        continue; //TODO обработать ситуацию, когда TryGenerateChunk возвращает false
                    }
                }
            }
            
            return states;

        }

        private void AddChunkToCache(ChunkPosition position, ChunkState state)
        {
            if (_loadedChunks.Count >= _options.AroundChunkRadius)
            {
                //TODO удалить из кэша самый дальний от игрока чанк
            }
            _loadedChunks[state.Position] = state;
        }

        /// <summary>
        /// Saves chunks states to storage
        /// </summary>
        /// <param name="newStates"></param>
        public void SaveChunks(ChunkState[] newStates)
        {
            SaveCacheToStorage();
            _existingChunks.Clear();
            _existingChunks.UnionWith(_chunkStorageService.GetStoredChunkPositions());


        }
        private void SaveCacheToStorage()
        {
            _chunkStorageService.SaveChunkState(_loadedChunks.ToDictionary(kv => kv.Key, kv => kv.Value.Data));
        }


    }
}