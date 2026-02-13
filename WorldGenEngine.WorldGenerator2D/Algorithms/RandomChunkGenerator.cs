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
        
        private TileData GenerateTile(int tileWorldX, int tileWorldY, int seed)
        {
            Random random = new Random(seed);
            return new TileData(1, random.Next(0, 100) % 2 == 0);
        }

        public ChunkData GenerateChunk(ChunkPosition position, int seed = 0)
        {
            var tiles = new TileData[ChunkData.ChunkSize * ChunkData.ChunkSize];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = GenerateTile(position.ChunkXPos * ChunkData.ChunkSize + i % ChunkData.ChunkSize,
                    position.ChunkYPos * ChunkData.ChunkSize + i / ChunkData.ChunkSize, seed);
            }

            return new ChunkData(tiles);
        }
    }
}