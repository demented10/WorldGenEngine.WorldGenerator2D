using System;
using WorldGenEngine.Core.GenerationMethods.Algorithms;
using WorldGenEngine.Core.MatrixGeneration.Generators;
using WorldGenEngine.WorldGenerator2D.Models;
using WorldGenEngine.WorldGenerator2D.Services.Generation;

namespace WorldGenEngine.WorldGenerator2D.Algorithms
{
    internal class PerlinNoiseChunkGenerator : IChunkGenerationAlgo
    {

        private readonly IBinaryMatrixGenerator _perlinNoiseMatrixGenerator;

        public PerlinNoiseChunkGenerator(IBinaryMatrixGenerator perlinNoiseMatrixGenerator)
        {
            _perlinNoiseMatrixGenerator = perlinNoiseMatrixGenerator;
        }
        public ChunkData GenerateChunk(ChunkPosition position, int seed = 0)
        {

            var matrix = _perlinNoiseMatrixGenerator.GenerateMatrix(ChunkData.ChunkSize, ChunkData.ChunkSize, seed);

            var tiles = new TileData[ChunkData.ChunkSize * ChunkData.ChunkSize];

            for (int y = 0; y < ChunkData.ChunkSize; y++)
            {
                for (int x = 0; x < ChunkData.ChunkSize; x++)
                {
                    tiles[y * ChunkData.ChunkSize + x] = new TileData(1, matrix[y* ChunkData.ChunkSize + x]);
                }
            }

            var data = new ChunkData(tiles);
            return data;
        }
    }
}