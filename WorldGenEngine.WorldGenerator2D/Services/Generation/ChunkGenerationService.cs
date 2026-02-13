using System;
using System.Collections.Generic;
using System.Text;
using WorldGenEngine.Core.GenerationMethods.Algorithms;
using WorldGenEngine.WorldGenerator2D.Models;
using WorldGenEngine.WorldGenerator2D.Services.Statements;

namespace WorldGenEngine.WorldGenerator2D.Services.Generation
{
    internal class ChunkGenerationService : IChunkGenerationService
    {
        private readonly IChunkGenerationAlgo _generationAlgo;

        public ChunkGenerationService(IChunkGenerationAlgo generationAlgo)
        {
            _generationAlgo = generationAlgo;
        }

        private ChunkData GenerateChunk(ChunkPosition position)
        {
            var chunkData = _generationAlgo.GenerateChunk(position);
            return chunkData;
        }

        public ChunkData[] GenerateChunkData(ChunkPosition[] positions)
        {
            var chunkDataArray = new ChunkData[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                chunkDataArray[i] = _generationAlgo.GenerateChunk(positions[i]);
            }

            return chunkDataArray;
        }
    }
}
