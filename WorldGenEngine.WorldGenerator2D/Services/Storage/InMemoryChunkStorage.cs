using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WorldGenEngine.WorldGenerator2D.Models;
using WorldGenEngine.WorldGenerator2D.Services.Statements;

namespace WorldGenEngine.WorldGenerator2D.Services.Storage
{
    internal class InMemoryChunkStorage : IChunkStorageService //Todo сделать асинхронным
                                                               //Todo добавить комментарии
    {
        private readonly ILogger<InMemoryChunkStorage> _logger;
        private readonly string _chunksDataFile = "chunks.bin";
        private readonly string _indexesFile = "chunks.idx";

        private Dictionary<ChunkPosition, long> _index;


        public InMemoryChunkStorage(ILogger<InMemoryChunkStorage> logger = null)
        {
            _logger = logger ?? NullLogger<InMemoryChunkStorage>.Instance;
            LoadIndex();
        }
        private void LoadIndex()
        {
            _index = new Dictionary<ChunkPosition, long>();

            if (!File.Exists(_indexesFile))
            {
                _logger.LogInformation("Index file not found, create empty index");
                return;
            }

            try
            {
                using (var reader = new BinaryReader(File.OpenRead(_indexesFile)))
                {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        int x = reader.ReadInt32();
                        int y = reader.ReadInt32();
                        long offset = reader.ReadInt64();
                        _index[new ChunkPosition(x, y)] = offset;
                    }
                }

                _logger.LogInformation($"Index was loaded, count: {_index.Count}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while index loading. Create new index");
                _index.Clear();
            }
        }

        private void SaveIndex()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(_indexesFile, FileMode.Create)))
            {
                writer.Write(_index.Count);
                foreach (var kvp in _index)
                {
                    writer.Write(kvp.Key.ChunkXPos);
                    writer.Write(kvp.Key.ChunkYPos);
                    writer.Write(kvp.Value);
                }
            }
            _logger.LogDebug("Index saved, count: {0}", _index.Count);
        }

        public void SaveChunkState(Dictionary<ChunkPosition, ChunkData> chunksData)
        {
            if (chunksData == null || chunksData.Count == 0)
                return;

            var newIndex = new Dictionary<ChunkPosition, long>();

            using (var writer = new BinaryWriter(File.Open(_chunksDataFile, FileMode.Create)))
            {
                foreach (var chunk in chunksData)
                {
                    long offset = writer.BaseStream.Position;
                    newIndex[chunk.Key] = offset;

                    writer.Write(chunk.Key.ChunkXPos);
                    writer.Write(chunk.Key.ChunkYPos);

                    writer.Write(chunk.Value.Tiles.Length);

                    foreach (var tile in chunk.Value.Tiles)
                    {
                        writer.Write(tile.TileId);
                        writer.Write(tile.IsSolid);
                    }
                }
            }

            _index = newIndex;
            SaveIndex();

            _logger.LogInformation("Save {0} chunks", chunksData.Count);

        }

       
        public Dictionary<ChunkPosition, ChunkData> LoadChunks(ChunkPosition[] positions)
        {
            var result  = new Dictionary<ChunkPosition, ChunkData>();

            if (positions == null || positions.Length == 0)
                return result;

            if (_index == null || _index.Count == 0)
            {
                LoadIndex();
                if (_index.Count == 0)
                {
                    _logger.LogWarning("Index is empty, nothing to load");
                    return result;
                }
            }

            if (!File.Exists(_chunksDataFile))
            {
                _logger.LogError("File with chunks data '{0}' not found", _chunksDataFile);
                return result;
            }

            using (BinaryReader reader = new BinaryReader(File.Open(_chunksDataFile, FileMode.OpenOrCreate)))
            {
                foreach (var pos in positions)
                {
                    if (_index.TryGetValue(pos, out long offset))
                    {
                        reader.BaseStream.Seek(offset, SeekOrigin.Begin);

                        int x = reader.ReadInt32();
                        int y = reader.ReadInt32();
                        int tileCount = reader.ReadInt32();

                        if (x != pos.ChunkXPos || y != pos.ChunkYPos)
                        {
                            _logger.LogError("Coordinate mismatch in data file: expected ({0},{1}), received ({2},{3})", pos.ChunkXPos, pos.ChunkYPos, x, y);
                            continue;
                        }

                        var tiles = new TileData[tileCount];
                        for (int i = 0; i < tileCount; i++)
                        {
                            int tileId = reader.ReadInt32();
                            bool isSolid = reader.ReadBoolean();
                            tiles[i] = new TileData(tileId, isSolid);
                        }

                        result[pos] = new ChunkData(tiles);
                    }
                    else
                    {
                        _logger.LogDebug("Chunk ({0},{1}) not found in index", pos.ChunkXPos, pos.ChunkYPos);
                    }
                }
            }
            _logger.LogInformation("Loaded {0} chunks of {1} requested", result.Count, positions.Length);

            return result;
        }

        public HashSet<ChunkPosition> GetStoredChunkPositions()
        {
            _logger.LogInformation("Loading stored chunk positions from file.");
           if(_index == null)
               LoadIndex();

            return new HashSet<ChunkPosition>(_index.Keys);
        }

        public ChunkData LoadChunkData(ChunkPosition position)
        {
            var result = LoadChunks(new[] { position });
            return result.TryGetValue(position, out var data) ? data : null;
        }
    }
}
