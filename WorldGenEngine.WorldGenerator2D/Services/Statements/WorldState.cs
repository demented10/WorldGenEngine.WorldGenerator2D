using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using WorldGenEngine.WorldGenerator2D.Factories;
using WorldGenEngine.WorldGenerator2D.Models;

namespace WorldGenEngine.WorldGenerator2D.Services.Statements
{
    [Serializable]
    public class WorldState //TODO Разделить реализацию на кэш и менеджер чанков
    {
        private readonly Dictionary<ChunkPosition, ChunkState> _loadedChunks;
        private readonly HashSet<ChunkPosition> _existingChunks;
        private readonly IChunkStorageService _chunkStorageService;
        private readonly IChunkGenerationServiceFactory _chunkGenerationServiceFactory;
        private readonly WorldStateOptions _options;
        private readonly ILogger<WorldState> _logger;
        private readonly WorldGenerationOptions _generationOptions;

        public WorldState(IChunkGenerationServiceFactory chunkGenerationServiceFactory, IChunkStorageService chunkStorageService, IOptions<WorldStateOptions> options, IOptions<WorldGenerationOptions> generationOptions, ILogger<WorldState> logger = null)
        {
            _chunkGenerationServiceFactory = chunkGenerationServiceFactory;
            _chunkStorageService = chunkStorageService;
            _generationOptions = generationOptions.Value;
            _logger = logger ?? NullLogger<WorldState>.Instance;
            _options = options.Value;
            _loadedChunks = new Dictionary<ChunkPosition, ChunkState>();
            _existingChunks = _chunkStorageService.GetStoredChunkPositions();
            _logger.LogInformation($"WorldState initialized with {_existingChunks.Count} existing chunks in storage.");
        }



        /// <summary>
        /// Check is chunk loaded in cache
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool CheckChunkIsLoaded(ChunkPosition position)
        {
            _logger.LogDebug($"Check is chunk loaded at position: {position.ToString()}");
            return _loadedChunks.ContainsKey(position);
        }
        /// <summary>
        /// Check is chunk exists in storage
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool CheckChunkExistsInStorage(ChunkPosition position)
        {
            _logger.LogDebug($"Check is chung exits in storage: {position.ToString()}");
            return _existingChunks.Contains(position);
        }



        /// <summary>
        /// Returns a box shaped array of chunk positions around the given chunk position.
        /// </summary>
        /// <param name="aroundChunk"></param>
        /// <returns></returns>
        private IEnumerable<ChunkPosition> GetAroundPositions(ChunkPosition aroundChunk)
        {
            _logger.LogDebug($"Getting around positions for chunk: {aroundChunk.ToString()} with radius: {_options.AroundChunkRadius}");
            for (int x = aroundChunk.ChunkXPos - _options.AroundChunkRadius; x <= aroundChunk.ChunkXPos + _options.AroundChunkRadius; x++)
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
                _logger.LogDebug($"Trying to load chunk from storage at position: {position.ToString()}");
                state = new ChunkState(position,_chunkStorageService.LoadChunkData(position));
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Failed to load chunk from storage at position: {position.ToString()}");
                state = default;
                return false;
            }
            
        }

        private bool TryGenerateChunk(ChunkPosition position, out ChunkState state)
        {
            try
            {
                _logger.LogDebug($"Trying to generate chunk at position: {position.ToString()}");
                state = new ChunkState(position, _chunkGenerationServiceFactory.Create(_generationOptions.GeneratorType).GenerateChunkData(position));
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Failed to generate chunk at position: {position.ToString()}");
                state = default;
                return false;
            }
        }
        private void AddChunkToCache(ChunkPosition position, ChunkState state)
        {
            _logger.LogDebug($"Adding chunk to cache at position: {position.ToString()}");
            if (_loadedChunks.Count >= _options.AroundChunkRadius)
            {
                //TODO удалить из кэша самый дальний от игрока чанк
            }
            _loadedChunks[state.Position] = state;
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
            _logger.LogInformation($"Getting chunk states around chunk: {aroundChunk.ToString()}");
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
                        _logger.LogError($"Chunk is marked as loaded in cache but failed to retrieve it at position: {chunkPosition.ToString()}");
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
                        _logger.LogError($"Chunk is marked as existing in storage but failed to load it at position: {chunkPosition.ToString()}");
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
                        _logger.LogError($"Failed to generate chunk at position: {chunkPosition.ToString()}");
                        continue; //TODO обработать ситуацию, когда TryGenerateChunk возвращает false
                    }
                }
            }
            
            return states;

        }


        /// <summary>
        /// Saves chunks states to storage
        /// </summary>
        /// <param name="newStates"></param>
        public void SaveChunks(ChunkState[] newStates)
        {
            _logger.LogInformation($"Saving {newStates.Length} chunks to storage.");
            SaveCacheToStorage();
            _existingChunks.Clear();
            _existingChunks.UnionWith(_chunkStorageService.GetStoredChunkPositions());
        }
        private void SaveCacheToStorage()
        {
            _logger.LogDebug($"Saving {_loadedChunks.Count} loaded chunks from cache to storage.");
            _chunkStorageService.SaveChunkState(_loadedChunks.ToDictionary(kv => kv.Key, kv => kv.Value.Data));
        }


    }
}