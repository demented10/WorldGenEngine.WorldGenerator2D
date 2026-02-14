using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using WorldGenEngine.WorldGenerator2D.Models;
using WorldGenEngine.WorldGenerator2D.Services.Statements;

namespace WorldGenEngine.WorldGenerator2D.Services.Storage
{
    internal class InMemoryChunkStorage : IChunkStorageService //Todo сделать асинхронным
    {
        private readonly ILogger<InMemoryChunkStorage> _logger;

        public InMemoryChunkStorage(ILogger<InMemoryChunkStorage> logger)
        {
            _logger = logger;
        }

        public void SaveChunkState(Dictionary<ChunkPosition, ChunkData> chunksData)
        {
            throw new NotImplementedException();
        }

        public Dictionary<ChunkPosition, ChunkData> LoadChunks(ChunkPosition[] position)
        {
            throw new NotImplementedException();
        }

        public HashSet<ChunkPosition> GetStoredChunkPositions()
        {
            _logger.LogInformation("Loading stored chunk positions from file.");
            var chunks = new HashSet<ChunkPosition>();
            using (BinaryReader reader =
                   new BinaryReader(System.IO.File.Open("chunk_positions.bin", FileMode.OpenOrCreate, FileAccess.Read)))
            {
               
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    int x = reader.ReadInt32();
                    int y = reader.ReadInt32();
                    chunks.Add(new ChunkPosition(x, y));
                }
            }

            return chunks;
        }

        public ChunkData LoadChunkData(ChunkPosition position)
        {
            throw new NotImplementedException();
        }
    }
}
