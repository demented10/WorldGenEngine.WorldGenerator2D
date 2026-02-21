using System;
using System.Collections.Generic;
using System.Text;
using WorldGenEngine.Core.MatrixGeneration.Generators;
using WorldGenEngine.WorldGenerator2D.Models;
using WorldGenEngine.WorldGenerator2D.Services.Generation;

namespace WorldGenEngine.WorldGenerator2D.Algorithms
{
    internal class RandomChunkGenerator : IChunkGenerationAlgo
    {
        public ChunkData GenerateChunk(ChunkPosition position, int seed = 0)
        {
            int chunkSeed = seed * 31 + position.ChunkXPos * 1323227 ^ position.ChunkYPos * 1365443;
            Random random = new Random();
            var tiles = new TileData[ChunkData.ChunkSize * ChunkData.ChunkSize];
            for (int i = 0; i < tiles.Length; i++)
            {
                int worldX = position.ChunkXPos * ChunkData.ChunkSize + i % ChunkData.ChunkSize;
                int worldY = position.ChunkYPos * ChunkData.ChunkSize + i / ChunkData.ChunkSize;
                bool isSolid = random.Next(0, 100) < 50;
                tiles[i] = new TileData(1, isSolid);
            }

            return new ChunkData(tiles);
        }
    }
}